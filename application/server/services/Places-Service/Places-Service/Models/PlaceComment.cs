using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Places_Service.Models;

public class PlaceComment
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [BsonElement("UserId")]
    [JsonPropertyName("UserId")]
    public string? UserId { get; set; }
    [BsonElement("PlaceId")]
    [JsonPropertyName("PlaceId")]
    public string? PlaceId { get; set; }
    [BsonElement("Text")]
    [JsonPropertyName("Text")]
    public string? Text { get; set; }
    [BsonElement("UpdatedAt")]
    [JsonPropertyName("UpdatedAt")]
    public DateTime? UpdatedAt { get; set; }
    [BsonElement("CreatedAt")]
    [JsonPropertyName("CreatedAt")]
    public DateTime CreatedAt { get; set; }
}