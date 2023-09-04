using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Reviews.Models;

public class Review
{   
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("UserId")]
    [JsonPropertyName("UserId")]
    public string? UserId { get; set; }
    
    [BsonElement("Rating")]
    [JsonPropertyName("Rating")]
    public float Rating { get; set; }

    [BsonElement("Text")]
    [JsonPropertyName("Text")]
    public string Text { get; set; } = null!;
    
    [BsonElement("UpdatedAt")]
    [JsonPropertyName("UpdatedAt")]
    public DateTime? UpdatedAt { get; set; }
    
    [BsonElement("CreatedAt")]
    [JsonPropertyName("CreatedAt")]
    public DateTime CreatedAt { get; set; }

}   