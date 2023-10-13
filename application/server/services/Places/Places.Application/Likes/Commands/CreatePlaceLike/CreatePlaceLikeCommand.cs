using MediatR;

namespace Places.Application.Likes.CreatePlaceLike;

public class CreatePlaceLikeCommand : IRequest<PlaceLikeViewModel>
{
    public string UserId { get; set; }
    public string PlaceId { get; set; }
}