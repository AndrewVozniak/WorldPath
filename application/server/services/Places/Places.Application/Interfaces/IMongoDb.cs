using Places.Domain;

namespace Places.Application.Interfaces;

public interface IMongoDb
{
    Task<Place> GetPlaceByName(string name);
    Task<Place> GetPlaceById(string id);
    Task AddOnePlaceAsync(Place place);
    Task AddManyPlacesAsync(IEnumerable<Place> places);
    Task AddOneParsedPlacePhoto(ParsedPlacePhoto photo);
    Task AddManyParsedPlacePhotos(IEnumerable<ParsedPlacePhoto> places);
    Task AddPlaceLikeAsync(PlaceLike placeLike);
    Task AddPlaceCommentAsync(PlaceComment placeComment);
    Task<PlaceLike> FindPlaceLikeAsync(string? placeId, string? userId);
    Task<bool> UpdatePlaceAsync(string id, Place updatePlace);
    Task<bool> PlaceExistsAsync(string? placeId);
    Task<IEnumerable<Place>?> FindPlacesNearbyAsync(double latitude, double longitude, int radius);
    Task DeletePlaceLikeAsync(string placeId, string userId);
    Task DeletePlaceAsync(string placeId);
}