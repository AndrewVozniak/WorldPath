namespace Places_Service.Data;

public class MongoDatabaseSettings
{
    public string ConnectionUri { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string PlaceCollection { get; set; } = null!;
    public string PlaceLikesCollection { get; set; } = null!;
    public string PlaceCommentsCollection { get; set; } = null!;

    public string ParsedPlacePhotosCollection { get; set; } = null!;
}