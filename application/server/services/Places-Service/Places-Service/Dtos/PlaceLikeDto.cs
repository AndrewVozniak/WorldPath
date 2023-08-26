using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Places_Service.Dtos;

public class PlaceLikeDto
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public int PlaceId { get; set; }
}