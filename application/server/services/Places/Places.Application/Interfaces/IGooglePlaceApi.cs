using Places.Application.Dtos;
using Refit;

namespace Places.Application.Interfaces;

public interface IGooglePlaceApi
{
    [Get("/nearbysearch/json?location={lat},{lon}&radius={radius}&key=AIzaSyCmCZMRuxBtuJmFRJAuGKhobIExrksG4l0")]
    Task<PlacesApiResponse> GetPlacesByCoordinate(float lat, float lon, 
        CancellationToken cancellationToken,int radius = 1000);
}