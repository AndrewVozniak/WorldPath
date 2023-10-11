using Microsoft.Build.Framework;

namespace Places_Service.Models;

public class PlaceCommentDto
{
    [Required] 
    public string UserId { get; set; } = null!;
    [Required] 
    public string PlaceId { get; set; } = null!;
    [Required] 
    public string Text { get; set; } = null!;
}