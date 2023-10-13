using MediatR;
using Places.Domain;

namespace Places.Application.Places.Queries.GetPlaceByCoordinate;

public class GetPlaceByCoordinateQuery : IRequest<Place>, IRequest<PlaceByCoordinatesViewModel>, IRequest<List<PlaceByCoordinatesViewModel>>
{
    public double Lat { get; set; }
    public double Lon { get; set; }
    public int Radius { get; set; }

    public GetPlaceByCoordinateQuery(double lat, double lon, int radius = 1000)
    {
        Lat = lat;
        Lon = lon;
        Radius = radius;
    }
}