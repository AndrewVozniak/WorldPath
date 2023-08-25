using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Places_Service.Models;

public class Place
{   
    [Key]
    public int Id { get; set; }
    [Required]
    public required string  Name { get; set; }
    [Required]
    public double Lat { get; set; }
    [Required]
    public double Lon { get; set; }
    [Required]
    public required string PlaceType { get; set; }
    public DateTime? UpdatedAt { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    
    // Navigation property for the related Place
    public IEnumerable<PlaceComment>? Comments { get; set; }
    public IEnumerable<PlaceLike>? Likes { get; set; }
}