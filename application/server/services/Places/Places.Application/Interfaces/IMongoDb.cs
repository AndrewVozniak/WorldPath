using Places.Application.Places.Commands.UpdatePlace;
using Places.Domain;

namespace Places.Application.Interfaces;

public interface IMongoDb
{
    Task<Place> GetPlaceByName(string name, CancellationToken cancellationToken);
    Task<Place> GetPlaceById(string id, CancellationToken cancellationToken);
    Task AddOnePlaceAsync(Place place, CancellationToken cancellationToken);
    Task AddManyPlacesAsync(IEnumerable<Place> places, CancellationToken cancellationToken);
    Task AddOneParsedPlacePhoto(ParsedPlacePhoto photo, CancellationToken cancellationToken);
    Task<ParsedPlacePhoto> FindParsedPlacePhoto(string id, CancellationToken cancellationToken);
    Task LikeParsedPlacePhoto(ParsedPlacePhotoLike parsedPlacePhotoLike);
    Task CommentParsedPlacePhoto(ParsedPlacePhotoComment comment);
    Task AddOneUploadedPlacePhoto(UploadedPlacePhoto uploadedPlacePhoto);
    Task<UploadedPlacePhoto> FindUploadedPlacePhoto(string id, CancellationToken cancellationToken);
    Task LikeUploadedPlacePhoto(UploadedPlacePhotoLike uploadedPlacePhotoLike);
    Task CommentUploadedPlacePhoto(UploadedPlacePhotoComment comment);
    Task AddManyUploadedPlacePhotos(IEnumerable<UploadedPlacePhoto> uploadedPlacePhotos,
        CancellationToken cancellationToken);
    Task AddManyParsedPlacePhotos(IEnumerable<ParsedPlacePhoto> places, CancellationToken cancellationToken);
    Task AddPlaceLikeAsync(PlaceLike placeLike, CancellationToken cancellationToken);
    Task AddPlaceCommentAsync(PlaceComment placeComment, CancellationToken cancellationToken);
    Task<PlaceLike> FindPlaceLikeAsync(string? placeId, string? userId, CancellationToken cancellationToken);
    Task<bool> UpdatePlaceAsync(UpdatePlaceCommand updatePlace, CancellationToken cancellationToken);
    Task<bool> PlaceExistsAsync(string? placeId, CancellationToken cancellationToken);
    Task<IEnumerable<Place>?> FindPlacesNearbyAsync(double latitude, double longitude, int radius,
        CancellationToken cancellationToken);
    Task DeletePlaceLikeAsync(string placeId, string userId, CancellationToken cancellationToken);
    Task DeletePlaceAsync(string placeId, CancellationToken cancellationToken);
}