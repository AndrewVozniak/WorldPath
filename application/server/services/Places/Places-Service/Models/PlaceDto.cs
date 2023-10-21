using AutoMapper;
using Microsoft.Build.Framework;
using Places.Application.Common.Mappings;
using Places.Application.Places.Commands.CreateOnePlace;

namespace Places_Service.Models;

public class PlaceDto : IMapWith<PlaceDto>
{
    [Required]
    public required string Name { get; set; }
    
    [Required]
    public double Lat { get; set; }
    
    [Required]
    public double Lon { get; set; }
    
    [Required]
    public required string PlaceType { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlaceDto, CreateOnePlaceCommand>();
    }
}