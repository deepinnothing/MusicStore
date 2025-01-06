using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MusicStore.API;
using MusicStore.API.Endpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Database connection
builder.Services.AddSingleton(
    new MongoClient($"mongodb+srv://{Environment.GetEnvironmentVariable("DB_USER")}:{Environment.GetEnvironmentVariable("DB_PASSWORD")}@mongocluster.yl7u1.mongodb.net/?retryWrites=true&w=majority&appName=MongoCluster")
        .GetDatabase(Environment.GetEnvironmentVariable("DB_NAME")));

// JWT configuration
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")!)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddAuthorization();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("admin", "true"));
});
builder.Services.AddTransient<JwtService>();

WebApplication app = builder.Build();

// Enabling JWT Auth
app.UseAuthentication();
app.UseAuthorization();

// Mapping Endpoints
app.MapStoreRoutes();
app.MapLibraryRoutes();
app.MapUserRoutes();

// Called when route is not found
app.MapFallback(() => Results.Json(new { message = "Not found!" }, statusCode: 404));

// Get PORT from Environment Variables or use the default one
string port = Environment.GetEnvironmentVariable("PORT") ?? "3000";

// Start server
app.Run($"http://localhost:{port}");