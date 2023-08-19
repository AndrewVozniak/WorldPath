namespace Routes.Models;

public class Place
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public float Lat { get; set; }
    public float Lon { get; set; }
}