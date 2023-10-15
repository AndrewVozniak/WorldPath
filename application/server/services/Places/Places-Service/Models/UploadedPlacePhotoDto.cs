namespace Places_Service.Models;

public class UploadedPlacePhotoDto
{
    public string PlaceId { get; set; }
    public byte[] PhotoData { get; set; }
}