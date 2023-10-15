using MediatR;

namespace Places.Application.UploadedPlacePhotos.Commands;

public class CreateUploadedPlacePhotoCommand : IRequest<UploadedPlacePhotoViewModel>
{
    public string PlaceId { get; set; }
    public byte[] PhotoData { get; set; }
}