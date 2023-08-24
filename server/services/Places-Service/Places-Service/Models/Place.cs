using System.Data;

namespace Places_Service.Models;

public class Place
{
    public int Id { get; set; }
    public string  Name { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string PlaceType { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}