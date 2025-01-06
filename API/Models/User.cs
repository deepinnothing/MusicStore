using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MusicStore.API.Models;

public class User
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("email")]
    public required string Email { get; set; }
    
    [BsonElement("password")]
    public required string Password { get; set; }
    
    [BsonElement("is_admin")]
    public bool IsAdmin { get; set; } = false;

    [BsonElement("library_tracks")]
    [BsonRepresentation(BsonType.ObjectId)]
    public List<string> LibraryTracks { get; set; } = [];
    
    [BsonElement("library_albums")]
    [BsonRepresentation(BsonType.ObjectId)]
    public List<string> LibraryAlbums { get; set; } = [];
}