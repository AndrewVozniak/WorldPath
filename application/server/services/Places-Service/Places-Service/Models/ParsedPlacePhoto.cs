using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Places_Service.Models;

public class ParsedPlacePhoto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("PlaceId")]
    [JsonPropertyName("PlaceId")]
    public string? PlaceId { get; set; }

    [BsonElement("PhotoPath")]
    [JsonPropertyName("PhotoPath")]
    public byte[] PhotoData { get; set; } = null!;
    
    [BsonElement("UpdatedAt")]
    [JsonPropertyName("UpdatedAt")]
    public DateTime? UpdatedAt { get; set; }
    
    [BsonElement("CreatedAt")]
    [JsonPropertyName("CreatedAt")]
    public DateTime CreatedAt { get; set; }
}