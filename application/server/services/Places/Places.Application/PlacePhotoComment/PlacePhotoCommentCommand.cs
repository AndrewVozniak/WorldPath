using MediatR;

namespace Places.Application.PlacePhotoComment;

public class PlacePhotoCommentCommand : IRequest<PlacePhotoCommentViewModel>
{
    public string PlacePhotoId { get; set; }
    public string UserId { get; set; }
    public string Text { get; set; }
}