using MusicStore.API.Controllers;

namespace MusicStore.API.Endpoints;

public static class UserEndpoints
{
    public static void MapUserRoutes(this WebApplication app)
    {
        app.MapPost("/signup", UserController.SignUpAsync);

        app.MapPost("/login", UserController.LogInAsync);
    }
}