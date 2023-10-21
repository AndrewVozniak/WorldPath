using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Places.Domain;

public class ParsedPlacePhoto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } = ObjectId.GenerateNewId().ToString();
    
    [BsonElement("PlaceId")]
    [JsonPropertyName("placeId")]
    public string? PlaceId { get; set; }
    
    [BsonElement("PhotoReference")]
    [JsonPropertyName("photo_reference")]
    public string? PhotoReference { get; set; }
    
    [BsonElement("UpdatedAt")]
    [JsonPropertyName("updatedAt")]
    public DateTime? UpdatedAt { get; set; }

    [BsonElement("CreatedAt")]
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}