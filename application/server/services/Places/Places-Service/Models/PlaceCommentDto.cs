using AutoMapper;
using Microsoft.Build.Framework;
using Places.Application.Comments.Commands;
using Places.Application.Common.Mappings;
using Places.Application.Likes.CreatePlaceLike;

namespace Places_Service.Models;

public class PlaceCommentDto : IMapWith<PlaceCommentDto>
{
    [Required] 
    public string UserId { get; set; } = null!;
    [Required] 
    public string PlaceId { get; set; } = null!;
    [Required] 
    public string Text { get; set; } = null!;
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlaceLikeDto, CreatePlaceCommentCommand>();
    }
}