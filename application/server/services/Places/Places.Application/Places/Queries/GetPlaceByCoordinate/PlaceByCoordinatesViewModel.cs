using AutoMapper;
using MongoDB.Driver.GeoJsonObjectModel;
using Places.Application.Common.Mappings;
using Places.Domain;

namespace Places.Application.Places.Queries.GetPlaceByCoordinate;

public class PlaceByCoordinatesViewModel : IMapWith<PlaceByCoordinatesViewModel>
{
    public string? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string PlaceType { get; set; } = string.Empty;
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Place, PlaceByCoordinatesViewModel>();
    }
}