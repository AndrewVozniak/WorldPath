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
    [JsonPropertyName("userId")]
    public string? UserId { get; set; }
    
    [BsonElement("Rating")]
    [JsonPropertyName("rating")]
    public float Rating { get; set; }

    [BsonElement("Text")]
    [JsonPropertyName("text")]
    public string Text { get; set; } = null!;
    
    [BsonElement("UpdatedAt")]
    [JsonPropertyName("updatedAt")]
    public DateTime? UpdatedAt { get; set; }
    
    [BsonElement("CreatedAt")]
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

}   