using System.ComponentModel.DataAnnotations;

namespace Places_Service.Models;

public class ParsedPlacePhoto
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int PlaceId { get; set; }
    [Required]
    public string PhotoPath { get; set; }
    public DateTime? UpdatedAt { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
}