using Places.Domain;

namespace Places.Application.PlacePhotos.Queries;

public class PlacePhotoViewModel
{
    public List<ParsedPlacePhoto> ParsedPlacePhotos { get; set; }
    public List<UploadedPlacePhoto> UploadedPlacePhotos { get; set; }
}