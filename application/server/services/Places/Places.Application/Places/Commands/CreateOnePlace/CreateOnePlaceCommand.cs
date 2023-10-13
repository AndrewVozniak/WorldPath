using MediatR;
using MongoDB.Driver.GeoJsonObjectModel;
using Places.Domain;

namespace Places.Application.Places.Commands.CreateOnePlace;

public class CreateOnePlaceCommand : IRequest, IRequest<Place>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string PlaceType { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
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
}