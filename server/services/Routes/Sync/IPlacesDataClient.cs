using Routes.Models;

namespace Routes.Sync;

public interface IPlacesDataClient
{
    Task<Place?> GetCoordinateFromPlaces(Coordinate coordinate);
}