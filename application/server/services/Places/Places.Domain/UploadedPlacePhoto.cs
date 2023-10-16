using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Places.Domain;

public class UploadedPlacePhoto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } = ObjectId.GenerateNewId().ToString();
    
    [BsonElement("PlaceId")]
    [JsonPropertyName("PlaceId")]
    public string? PlaceId { get; set; }
    
    [BsonElement("PhotoData")]
    [JsonPropertyName("PhotoData")]
    public byte[] PhotoData { get; set; }
    
    [BsonElement("UpdatedAt")]
    [JsonPropertyName("UpdatedAt")]
    public DateTime? UpdatedAt { get; set; }

    [BsonElement("CreatedAt")]
    [JsonPropertyName("CreatedAt")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}