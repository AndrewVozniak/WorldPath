namespace Routes.Services;

public interface ITrafficService
{
    Task<HttpResponseMessage> GetTraffic(float lat, float lon, int radius = 5000);
}