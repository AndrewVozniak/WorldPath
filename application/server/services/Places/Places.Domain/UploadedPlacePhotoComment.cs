using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Places.Domain;

public class UploadedPlacePhotoComment
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } = ObjectId.GenerateNewId().ToString();
    
    [BsonElement("UserId")]
    [JsonPropertyName("UserId")]
    public string? UserId { get; set; }
    
    [BsonElement("PlacePhotoId")]
    [JsonPropertyName("PlacePhotoId")]
    public string? PlacePhotoId { get; set; }
    
    [BsonElement("Text")]
    [JsonPropertyName("Text")]
    public string? Text { get; set; }
    
    [BsonElement("UpdatedAt")]
    [JsonPropertyName("UpdatedAt")]
    public DateTime? UpdatedAt { get; set; }
    
    [BsonElement("CreatedAt")]
    [JsonPropertyName("CreatedAt")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}