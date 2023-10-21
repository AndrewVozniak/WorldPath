using AutoMapper;
using Places.Application.Comments.Commands;
using Places.Application.Common.Mappings;

namespace Places_Service.Models;

public class PlacePhotoLikeDto : IMapWith<PlacePhotoLikeDto>
{
    public string PlacePhotoId { get; set; }
    public string UserId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlacePhotoCommentDto, CreatePlaceCommentCommand>();
    }
}