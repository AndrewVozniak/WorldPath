using Microsoft.AspNetCore.Mvc;
using Places_Service.Models;

namespace Places_Service.Services;

public interface IGooglePlaceService
{
    Task<PlacesApiResponse> GetPlaceByCoordinate(float lat, float lon, int radius = 1000);
}