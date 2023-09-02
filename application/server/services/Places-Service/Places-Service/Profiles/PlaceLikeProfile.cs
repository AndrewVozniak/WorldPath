using AutoMapper;
using Places_Service.Dtos;
using Places_Service.Models;

namespace Places_Service.Profiles;

public class PlaceLikeProfile : Profile
{
    public PlaceLikeProfile()
    {
        // Source - Target
        CreateMap<PlaceLikeDto, PlaceLike>();
    }
}