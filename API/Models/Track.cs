using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MusicStore.API.Models;

public class Track
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("title")]
    public required string Title { get; set; }
    
    [BsonElement("artist")]
    public required string Artist { get; set; }
    
    [BsonElement("year")]
    public required int Year { get; set; }
    
    [BsonElement("length")]
    public required int Length { get; set; }
}

public class TrackUpdate
{
    [BsonElement("title")]
    public string? Title { get; set; }
    
    [BsonElement("artist")]
    public string? Artist { get; set; }
    
    [BsonElement("year")]
    public int? Year { get; set; }
    
    [BsonElement("length")]
    public int? Length { get; set; }
}