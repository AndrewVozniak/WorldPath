namespace Routes_Service.Dtos;

public class RouteRequest
{
    public string StartLocation { get; set; } = string.Empty;
    public string EndLocation { get; set; } = string.Empty;
    public TravelMode Mode { get; set; }
    public string Key { get; set; } = string.Empty;

    public enum TravelMode
    {
        Driving,
        Walking,
        Bicycling,
        Transit
    }
}