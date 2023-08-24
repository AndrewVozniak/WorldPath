namespace Places_Service.Models;

public class Result
{
    public Geometry Geometry { get; set; }
    public string Name { get; set; }
    public OpeningHours OpeningHours { get; set; }
    public List<Photo> Photos { get; set; }
    public string PlaceId { get; set; }
    public double Rating { get; set; }
    public string Vicinity { get; set; }
    public List<string> Types { get; set; }
}