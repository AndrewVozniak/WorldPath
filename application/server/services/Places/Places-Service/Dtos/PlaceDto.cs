using Microsoft.Build.Framework;

namespace Places_Service.Dtos;

public class PlaceDto
{   
    [Required]
    public required string Name { get; set; }
    
    [Required]
    public double Lat { get; set; }
    
    [Required]
    public double Lon { get; set; }
    
    [Required]
    public required string PlaceType { get; set; }
}