namespace Places.Application.UploadedPlacePhotos.Commands;

public class UploadedPlacePhotoViewModel
{
    public string Id { get; set; }
    public string PlaceId { get; set; }
    public byte[] PhotoData { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}