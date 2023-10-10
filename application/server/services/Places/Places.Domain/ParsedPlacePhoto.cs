using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Places.Domain;

public class ParsedPlacePhoto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("PlaceId")]
    [JsonPropertyName("placeId")]
    public string? PlaceId { get; set; }
    
    [BsonElement("PhotoPath")]
    [JsonPropertyName("photoPath")]
    public string? PhotoPath { get; set; }
    
    [BsonElement("UpdatedAt")]
    [JsonPropertyName("updatedAt")]
    public DateTime? UpdatedAt { get; set; }
    
    [BsonElement("CreatedAt")]
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }
}