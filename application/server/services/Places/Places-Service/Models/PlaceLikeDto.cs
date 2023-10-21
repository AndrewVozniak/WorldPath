using AutoMapper;
using Microsoft.Build.Framework;
using Places.Application.Common.Mappings;
using Places.Application.Likes.CreatePlaceLike;

namespace Places_Service.Models;

public class PlaceLikeDto : IMapWith<PlaceLikeDto>
{
    [Required] 
    public string UserId { get; set; } = null!;
    [Required] 
    public string PlaceId { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlaceLikeDto, CreatePlaceLikeCommand>();
    }
}