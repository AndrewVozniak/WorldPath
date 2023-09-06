using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Places_Service.Dtos;

public class PlaceLikeDto
{
    [Required] public string UserId { get; set; } = null!;
    [Required] public string PlaceId { get; set; } = null!;
}