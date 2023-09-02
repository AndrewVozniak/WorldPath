using AutoMapper;
using Places_Service.Dtos;
using Places_Service.Models;

namespace Places_Service.Profiles;

public class PlaceProfile : Profile
{
    public PlaceProfile()
    {   
        // Source -> Target
        CreateMap<PlaceDto, Place>();
    }
}