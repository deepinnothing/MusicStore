using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MusicStore.API.Models;

namespace MusicStore.API.Controllers;

public static class LibraryController
{
    public static async Task<IResult> GetAllTracks(ClaimsPrincipal user, IMongoDatabase database)
    {
        try
        {
            // Get user id from the JWT token
            string? userId = user.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId)) return Results.Unauthorized();
            
            // Find user in the database to get user's library
            IMongoCollection<User>? usersCollection = database.GetCollection<User>("users");
            FilterDefinition<User>? filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            User? foundUser = await usersCollection.Find(filter).FirstOrDefaultAsync();
            if (foundUser == null) return Results.Unauthorized();
            
            // Find tracks from user's library in tracks collection to return more useful info instead of just track ids
            IMongoCollection<Track>? tracksCollection = database.GetCollection<Track>("tracks");
            List<Track>? tracks = await tracksCollection.Find(t => foundUser.LibraryTracks.Contains(t.Id!)).ToListAsync();
                
            return Results.Ok(tracks);
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(ex.Message);
        }
    }

    public static async Task<IResult> AddTrack([FromBody] string trackId, ClaimsPrincipal user, IMongoDatabase database)
    {
        try
        {
            // Get user id from the JWT token
            string? userId = user.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId)) return Results.Unauthorized();
            
            // Find user in the database to get access to the user's library
            IMongoCollection<User>? usersCollection = database.GetCollection<User>("users");
            FilterDefinition<User>? filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            
            // Add track to user's library by id if it's not already added
            UpdateDefinition<User>? updateDefinition = Builders<User>.Update.AddToSet(u => u.LibraryTracks, trackId);
            UpdateResult? result = await usersCollection.UpdateOneAsync(filter, updateDefinition);
            return result.MatchedCount == 0 ? Results.Unauthorized() : Results.Ok($"Track with id {trackId} successfully added to the library.");
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(ex.Message);
        }
    }
    
    public static async Task<IResult> RemoveTrack([FromBody] string trackId, ClaimsPrincipal user, IMongoDatabase database)
    {
        try
        {
            // Get user id from the JWT token
            string? userId = user.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId)) return Results.Unauthorized();
            
            // Find user in the database to get access to the user's library
            IMongoCollection<User>? usersCollection = database.GetCollection<User>("users");
            FilterDefinition<User>? filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            
            // Remove track from user's library by id
            UpdateDefinition<User>? updateDefinition = Builders<User>.Update.Pull(u => u.LibraryTracks, trackId);
            UpdateResult? result = await usersCollection.UpdateOneAsync(filter, updateDefinition);
            return result.MatchedCount == 0 ? Results.Unauthorized() : 
                result.ModifiedCount == 0 ? Results.BadRequest($"No track in the library with id {trackId}") : 
                Results.Ok($"Track with id {trackId} successfully removed from the library.");
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(ex.Message);
        }
    }
    
    public static async Task<IResult> GetAllAlbums(ClaimsPrincipal user, IMongoDatabase database)
    {
        try
        {
            // Get user id from the JWT token
            string? userId = user.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId)) return Results.Unauthorized();
            
            // Find user in the database to get user's library
            IMongoCollection<User>? usersCollection = database.GetCollection<User>("users");
            FilterDefinition<User>? filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            User? foundUser = await usersCollection.Find(filter).FirstOrDefaultAsync();
            if (foundUser == null) return Results.Unauthorized();
            
            // Find albums from user's library in albums collection to return more useful info instead of just album ids
            // When requesting multiple albums tracks are usually not needed and create too much boilerplate
            IMongoCollection<Album>? albumsCollection = database.GetCollection<Album>("albums");
            List<BsonDocument>? albums = await albumsCollection.Find(t => foundUser.LibraryAlbums.Contains(t.Id!))
                .Project(Builders<Album>.Projection.Exclude("tracks")).ToListAsync();
                
            // Need to manually convert ObjectId to string
            return Results.Ok(albums.Select(bson =>
            {
                bson["_id"] = bson["_id"].ToString();
                return bson.ToDictionary();
            }).ToList());
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(ex.Message);
        }
    }

    public static async Task<IResult> AddAlbum([FromBody] string albumId, ClaimsPrincipal user, IMongoDatabase database)
    {
        try
        {
            // Get user id from the JWT token
            string? userId = user.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId)) return Results.Unauthorized();
            
            // Find user in the database to get user's library
            IMongoCollection<User>? usersCollection = database.GetCollection<User>("users");
            FilterDefinition<User>? filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            
            // Add album to user's library by id if it's not already added
            UpdateDefinition<User>? updateDefinition = Builders<User>.Update.AddToSet(u => u.LibraryAlbums, albumId);
            UpdateResult? result = await usersCollection.UpdateOneAsync(filter, updateDefinition);
            return result.MatchedCount == 0 ? Results.Unauthorized() : Results.Ok($"Album with id {albumId} successfully added to the library.");
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(ex.Message);
        }
    }
    
    public static async Task<IResult> RemoveAlbum([FromBody] string albumId, ClaimsPrincipal user, IMongoDatabase database)
    {
        try
        {
            // Get user id from the JWT token
            string? userId = user.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId)) return Results.Unauthorized();
            
            // Find user in the database to get user's library
            IMongoCollection<User>? usersCollection = database.GetCollection<User>("users");
            FilterDefinition<User>? filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            
            // Remove album from user's library by id
            UpdateDefinition<User>? updateDefinition = Builders<User>.Update.Pull(u => u.LibraryAlbums, albumId);
            UpdateResult? result = await usersCollection.UpdateOneAsync(filter, updateDefinition);
            return result.MatchedCount == 0 ? Results.Unauthorized() : 
                result.ModifiedCount == 0 ? Results.BadRequest($"No album in the library with id {albumId}") : 
                Results.Ok($"Album with id {albumId} successfully removed from the library.");
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(ex.Message);
        }
    }
}