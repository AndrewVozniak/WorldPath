using AutoMapper;
using Microsoft.Build.Framework;
using Places.Application.Common.Mappings;
using Places.Application.Places.Commands.UpdatePlace;

namespace Places_Service.Models;

public class UpdatePlaceDto : IMapWith<UpdatePlaceDto>
{   
    [Required]
    public required string Id { get; set; }
    
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
        profile.CreateMap<UpdatePlaceDto, UpdatePlaceCommand>();
    }
}