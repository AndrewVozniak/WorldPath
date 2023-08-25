using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Places_Service.Models;

public class PlaceComment
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    [ForeignKey("Place")]
    public int PlaceId { get; set; }
    public required string Text { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    
    // Navigation property for the related Place
    public Place Place { get; set; }
}