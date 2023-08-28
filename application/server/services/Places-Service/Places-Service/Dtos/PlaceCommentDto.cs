using System.ComponentModel.DataAnnotations;

namespace Places_Service.Dtos;

public class PlaceCommentDto
{
    [Required] 
    public int UserId { get; set; }
    [Required]
    public int PlaceId { get; set; }
    [Required]
    public string Text { get; set; }
}