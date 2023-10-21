using AutoMapper;
using Places.Application.Comments.Commands;
using Places.Application.Common.Mappings;
using Places.Domain;

namespace Places.Application.PlacePhotoComment;

public class PlacePhotoCommentViewModel : IMapWith<PlaceCommentViewModel>
{
    public string Id { get; set; }
    public string PlacePhotoId { get; set; }
    public string UserId { get; set; }
    public string Text { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlaceComment, PlaceCommentViewModel>();
    }
}