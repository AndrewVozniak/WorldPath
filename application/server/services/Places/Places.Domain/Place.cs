using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace Places.Domain;

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
    
    [BsonElement("Location")]
    [JsonPropertyName("Location")]
    public GeoJsonPoint<GeoJson2DGeographicCoordinates> Location
    {
        get => new GeoJsonPoint<GeoJson2DGeographicCoordinates>(
            new GeoJson2DGeographicCoordinates(Lon, Lat)
        );
        set
        {
            Lon = value.Coordinates.Longitude;
            Lat = value.Coordinates.Latitude;
        }
    }
    
    [BsonIgnore]
    public string? PhotoReference { get; set; }
}