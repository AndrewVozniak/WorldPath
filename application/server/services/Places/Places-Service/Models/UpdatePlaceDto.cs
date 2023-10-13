using Microsoft.Build.Framework;

namespace Places_Service.Models;

public class UpdatePlaceDto
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
}