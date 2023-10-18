using MediatR;

namespace Places.Application.PlacePhotoLike.Commands;

public class PlacePhotoLikeCommand : IRequest<PlacePhotoLikeViewModel>
{
    public string PlacePhotoId { get; set; }
    public string UserId { get; set; }
}