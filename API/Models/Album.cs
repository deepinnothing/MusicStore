using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MusicStore.API.Models;

public class Album
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("title")]
    public required string Title { get; set; }
    
    [BsonElement("artist")]
    public required string Artist { get; set; }
    
    [BsonElement("year")]
    public required int Year { get; set; }
    
    [BsonElement("tracks")]
    [BsonRepresentation(BsonType.ObjectId)]
    public List<string> Tracks { get; set; } = [];
    
    [BsonElement("tracks_number")]
    public int TracksNumber => Tracks.Count;
}

public class AlbumUpdate
{
    [BsonElement("title")]
    public string? Title { get; set; }
    
    [BsonElement("artist")]
    public string? Artist { get; set; }
    
    [BsonElement("year")]
    public int? Year { get; set; }
    
    [BsonElement("tracks")]
    [BsonRepresentation(BsonType.ObjectId)]
    public List<string>? Tracks { get; set; }
}