using AutoMapper;
using Places.Application.Comments.Commands;
using Places.Application.Common.Mappings;

namespace Places_Service.Models;

public class PlacePhotoCommentDto : IMapWith<PlacePhotoCommentDto>
{
    public string PlacePhotoId { get; set; }
    public string UserId { get; set; }
    public string Text { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlaceCommentDto, CreatePlaceCommentCommand>();
    }
}