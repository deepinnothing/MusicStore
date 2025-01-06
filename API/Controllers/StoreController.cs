using MongoDB.Driver;
using MusicStore.API.Models;

namespace MusicStore.API.Controllers;

public static class StoreController
{
    public static async Task<IResult> AddNewTrack(Track track, IMongoDatabase database)
    {
        try
        {
            IMongoCollection<Track>? tracksCollection = database.GetCollection<Track>("tracks");
            await tracksCollection.InsertOneAsync(track);
            return Results.Created($"store/tracks/{track.Id}", new
            {
                message = "Added new track.",
                data = track
            });
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(ex.Message);
        }
    }

    public static async Task<IResult> GetAllTracks(IMongoDatabase database)
    {
        try
        {
            IMongoCollection<Track>? tracksCollection = database.GetCollection<Track>("tracks");
            // An empty filter to find all documents in the collection
            FilterDefinition<Track>? filter = Builders<Track>.Filter.Empty;
            
            List<Track>? tracks = await tracksCollection.Find(filter).ToListAsync();
                
            return Results.Ok(tracks);
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(ex.Message);
        }
    }

    public static async Task<IResult> ModifyTrack(string id, TrackUpdate trackUpdate, IMongoDatabase database)
    {
        try
        {
            // Update only the fields provided by the client
            List<UpdateDefinition<Track>> updates = [];

            if (!string.IsNullOrEmpty(trackUpdate.Title))
            {
                updates.Add(Builders<Track>.Update.Set("title", trackUpdate.Title));
            }

            if (!string.IsNullOrEmpty(trackUpdate.Artist))
            {
                updates.Add(Builders<Track>.Update.Set("artist", trackUpdate.Artist));
            }

            if (trackUpdate.Year.HasValue)
            {
                updates.Add(Builders<Track>.Update.Set("year", trackUpdate.Year.Value));
            }

            if (trackUpdate.Length.HasValue)
            {
                updates.Add(Builders<Track>.Update.Set("length", trackUpdate.Length.Value));
            }

            if (updates.Count == 0) 
                return Results.BadRequest("No fields provided to update.");
                
            IMongoCollection<Track>? tracksCollection = database.GetCollection<Track>("tracks");
            // Update the track with the specified id if one exists
            FilterDefinition<Track>? filter = Builders<Track>.Filter.Eq(t => t.Id, id);
            UpdateDefinition<Track>? updateDefinition = Builders<Track>.Update.Combine(updates);
            UpdateResult? result = await tracksCollection.UpdateOneAsync(filter, updateDefinition);
                
            return result.MatchedCount == 0 ? Results.NotFound($"Track with id {id} not found.") : Results.Ok($"Track with id {id} successfully updated.");
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(ex.Message);
        }
    }

    public static async Task<IResult> DeleteTrack(string id, IMongoDatabase database)
    {
        try
        {
            IMongoCollection<Track>? tracksCollection = database.GetCollection<Track>("tracks");
            FilterDefinition<Track>? filter = Builders<Track>.Filter.Eq(t => t.Id, id);
                
            DeleteResult? result = await tracksCollection.DeleteOneAsync(filter);
                
            return result.DeletedCount == 0 ? Results.NotFound($"Track with id {id} not found.") : Results.Ok($"Track with id {id} successfully deleted.");
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(ex.Message);
        }
    }
    
    public static async Task<IResult> GetTrackById(string id, IMongoDatabase database)
    {
        try
        {
            IMongoCollection<Track>? tracksCollection = database.GetCollection<Track>("tracks");
            FilterDefinition<Track>? filter = Builders<Track>.Filter.Eq(t => t.Id, id);
                
            Track? track = await tracksCollection.Find(filter).FirstOrDefaultAsync();
                
            return track is null ? Results.NotFound($"Track with id {id} not found.") : Results.Ok(track);
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(ex.Message);
        }
    }
    
    public static async Task<IResult> AddNewAlbum(Album album, IMongoDatabase database)
    {
        try
        {
            IMongoCollection<Album>? tracksCollection = database.GetCollection<Album>("albums");
            await tracksCollection.InsertOneAsync(album);
            return Results.Created($"store/tracks/{album.Id}", new
            {
                message = "Added new album.",
                data = album
            });
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(ex.Message);
        }
    }

    public static async Task<IResult> GetAllAlbums(IMongoDatabase database)
    {
        try
        {
            IMongoCollection<Album>? albumsCollection = database.GetCollection<Album>("albums");
            FilterDefinition<Album>? filter = Builders<Album>.Filter.Empty;
                
            List<Album>? albums = await albumsCollection.Find(filter).ToListAsync();
                
            return Results.Ok(albums);
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(ex.Message);
        }
    }

    public static async Task<IResult> ModifyAlbum(string id, AlbumUpdate albumUpdate, IMongoDatabase database)
    {
        try
        {
            List<UpdateDefinition<Album>> updates = [];

            if (!string.IsNullOrEmpty(albumUpdate.Title))
            {
                updates.Add(Builders<Album>.Update.Set("title", albumUpdate.Title));
            }

            if (!string.IsNullOrEmpty(albumUpdate.Artist))
            {
                updates.Add(Builders<Album>.Update.Set("artist", albumUpdate.Artist));
            }

            if (albumUpdate.Year.HasValue)
            {
                updates.Add(Builders<Album>.Update.Set("year", albumUpdate.Year.Value));
            }

            if (albumUpdate.Tracks != null)
            {
                updates.Add(Builders<Album>.Update.Set("tracks", albumUpdate.Tracks));
            }

            if (updates.Count == 0) 
                return Results.BadRequest("No fields provided to update.");
                
            IMongoCollection<Album>? albumsCollection = database.GetCollection<Album>("albums");
            FilterDefinition<Album>? filter = Builders<Album>.Filter.Eq(t => t.Id, id);
            UpdateDefinition<Album>? updateDefinition = Builders<Album>.Update.Combine(updates);
            UpdateResult? result = await albumsCollection.UpdateOneAsync(filter, updateDefinition);
                
            return result.MatchedCount == 0 ? Results.NotFound($"Album with id {id} not found.") : Results.Ok($"Album with id {id} successfully updated.");
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(ex.Message);
        }
    }

    public static async Task<IResult> DeleteAlbum(string id, IMongoDatabase database)
    {
        try
        {
            IMongoCollection<Album>? albumsCollection = database.GetCollection<Album>("albums");
            FilterDefinition<Album>? filter = Builders<Album>.Filter.Eq(t => t.Id, id);
                
            DeleteResult? result = await albumsCollection.DeleteOneAsync(filter);
                
            return result.DeletedCount == 0 ? Results.NotFound($"Album with id {id} not found.") : Results.Ok($"Album with id {id} successfully deleted.");
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(ex.Message);
        }
    }
    
    public static async Task<IResult> GetAlbumById(string id, IMongoDatabase database)
    {
        try
        {
            IMongoCollection<Album>? albumsCollection = database.GetCollection<Album>("albums");
            FilterDefinition<Album>? filter = Builders<Album>.Filter.Eq(t => t.Id, id);
                
            Album? album = await albumsCollection.Find(filter).FirstOrDefaultAsync();
                
            return album is null ? Results.NotFound($"Album with id {id} not found.") : Results.Ok(album);
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(ex.Message);
        }
    }
}