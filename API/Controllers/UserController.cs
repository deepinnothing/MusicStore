using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using MusicStore.API.Models;

namespace MusicStore.API.Controllers;

public static class UserController
{
    public static async Task<IResult> SignUpAsync(User user, IMongoDatabase database)
    {
        try
        {
            // Do not let new users become admins
            if (user.IsAdmin) return Results.Forbid();
            
            IMongoCollection<User>? usersCollection = database.GetCollection<User>("users");
            // Replace the password with a hash generated for it
            user.Password = new PasswordHasher<object?>().HashPassword(null, user.Password);
            await usersCollection.InsertOneAsync(user);
            return Results.Json($"Added new user with id {user.Id}.", statusCode: 201);
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(ex.Message);
        }
    }

    public static async Task<IResult> LogInAsync(User user, IMongoDatabase database, JwtService jwt)
    {
        try
        {
            IMongoCollection<User>? usersCollection = database.GetCollection<User>("users");
            // Check if user with the specified email exists    
            FilterDefinition<User>? filter = Builders<User>.Filter.Eq(u => u.Email, user.Email);
            User? foundUser = await usersCollection.Find(filter).FirstOrDefaultAsync();
            if (foundUser == null) return Results.Unauthorized();
            // Check if provided password is correct    
            PasswordVerificationResult passwordVerificationResult = new PasswordHasher<object?>().VerifyHashedPassword(null, foundUser.Password, user.Password);
            // If the password is correct, return JWT token
            return passwordVerificationResult == PasswordVerificationResult.Failed ? Results.Unauthorized() : Results.Ok(jwt.CreateToken(foundUser));
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(ex.Message);
        }
    }
}