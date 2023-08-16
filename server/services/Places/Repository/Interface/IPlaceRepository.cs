using Places.Models;

namespace Places.Repository.Interface;

public interface IPlaceRepository
{
    Task<List<Place>> GetPlacesAsync();
    Task<Place?> GetPlaceByIdAsync(int placeId);
    Task InsertPlacesAsync(Place place);
    Task UpdatePlacesAsync(Place place);
    Task DeletePlacesAsync(int placeId);
    Task SaveAsync();
}