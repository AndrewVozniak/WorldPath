using Places.Application.Dtos;

namespace Places.Application.Interfaces;

public interface IGooglePlaceService
{
    Task<PlacesApiResponse> GetPlaceByCoordinate(float lat, float lon, int radius = 1000);
}