using AutoMapper;
using Places.Domain;

namespace Places.Application.Comments.Commands;

public class PlaceCommentViewModel
{
    public string? Id { get; set; }
    public string? UserId { get; set; }
    public string? PlaceId { get; set; }
    public string? Text { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public void Mapper(Profile profile)
    {
        profile.CreateMap<PlaceComment, PlaceCommentViewModel>();
    }
}