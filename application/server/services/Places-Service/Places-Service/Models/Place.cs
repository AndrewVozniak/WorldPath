using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Places_Service.Models;

public class Place
{   
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("Name")]
    [JsonPropertyName("Name")]
    public string Name { get; set; } = string.Empty;
    
    [BsonElement("Lat")]
    [JsonPropertyName("Lat")]
    public double Lat { get; set; }
    
    [BsonElement("Lon")]
    [JsonPropertyName("Lon")]
    public double Lon { get; set; }

    [BsonElement("PlaceType")]
    [JsonPropertyName("PlaceType")]
    public string PlaceType { get; set; } = string.Empty;
    
    [BsonElement("UpdatedAt")]
    [JsonPropertyName("UpdatedAt")]
    public DateTime? UpdatedAt { get; set; }
    
    [BsonElement("CreatedAt")]
    [JsonPropertyName("CreatedAt")]
    public DateTime CreatedAt { get; set; }
    
}