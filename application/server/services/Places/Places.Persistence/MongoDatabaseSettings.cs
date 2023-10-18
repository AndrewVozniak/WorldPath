namespace Places.Persistence;

public class MongoDatabaseSettings
{
    public string ConnectionUri { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string PlaceCollection { get; set; } = null!;
    public string PlaceLikesCollection { get; set; } = null!;
    public string PlaceCommentsCollection { get; set; } = null!;
    public string ParsedPlacePhotosCollection { get; set; } = null!;
    public string ParsedPlacePhotoLikesCollection { get; set; } = null!;
    public string ParsedPlacePhotoCommentsCollection { get; set; } = null!;
    public string UploadedPlacePhotosCollection { get; set; } = null!;
    public string UploadedPlacePhotoLikesCollection { get; set; } = null!;
    public string UploadedPlacePhotoCommentsCollection { get; set; } = null!;
}