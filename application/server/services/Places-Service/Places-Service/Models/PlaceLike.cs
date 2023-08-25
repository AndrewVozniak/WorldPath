﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Places_Service.Models;

public class PlaceLike
{
    [Key] 
    public int Id { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    [ForeignKey("Place")]
    public int PlaceId { get; set; }
    public Place Place { get; set; }
    public DateTime? UpdatedAt { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
}