using AutoMapper;
using Places.Application.Common.Mappings;
using Places.Application.Places.Commands.CreateManyPlaces;

namespace Places.Application.Dtos;

public class PlacesApiResponse : IMapWith<PlacesApiResponse>
{
    public List<Result> Results { get; set; }
    public string Status { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlacesApiResponse, CreateManyPlacesCommand>()
            .ForMember(command => command.Results,
                opt => opt.MapFrom(response => response.Results))
            .ForMember(command => command.Status,
                opt => opt.MapFrom(response => response.Status))
            .ForMember(command => command.Results[0].Photos, 
                opt => opt.MapFrom(response => response.Results[0].Photos));;
    }
}