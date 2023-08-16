using System.ComponentModel.DataAnnotations;

namespace Places.Models;

public class Place
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public float Lat { get; set; }
    public float Lon { get; set; }
}