using MediatR;

namespace Places.Application.Comments.Commands;

public class CreatePlaceCommentCommand : IRequest<PlaceCommentViewModel>
{
    public string UserId { get; set; }
    public string PlaceId { get; set; }
    public string Text { get; set; }

    public CreatePlaceCommentCommand(string userId, string placeId, string text)
    {
        UserId = userId;
        PlaceId = placeId;
        Text = text;
    }
}