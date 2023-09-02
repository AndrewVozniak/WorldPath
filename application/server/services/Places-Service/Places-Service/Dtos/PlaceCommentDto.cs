using System.ComponentModel.DataAnnotations;

namespace Places_Service.Dtos;

public class PlaceCommentDto
{
    [Required] public string UserId { get; set; } = null!;
    [Required] public string PlaceId { get; set; } = null!;
    [Required] public string Text { get; set; } = null!;
}