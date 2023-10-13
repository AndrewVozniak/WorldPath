namespace Places.Application.Places.Queries.GetPlaceById;

public class PlaceByIdViewModel
{
    public string? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string PlaceType { get; set; } = string.Empty;
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}