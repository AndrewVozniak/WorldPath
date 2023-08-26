using Places_Service.Models;

namespace Places_Service.Repository.Interfaces;

public interface IPlaceRepository
{
    Task<Place?> GetPlaceByCoordinate(float lat, float lon);
    Task<Place?> GetPlacesById(int id);
    Task<Place?> GetPlaceByName(string name);
    Task SaveAsync();
}