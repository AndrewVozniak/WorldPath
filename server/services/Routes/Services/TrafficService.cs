namespace Routes.Services;

public class TrafficService : ITrafficService
{
    private readonly HttpClient _httpClient;

    public TrafficService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<HttpResponseMessage> GetTraffic(float lat, float lon, int radius = 5000)
    {
        var overpassUrl = "http://overpass-api.de/api/interpreter";
        var overpassQuery = $"""
                             [out:json];
                             way(around:{radius},{lat},{lon})["highway"];
                             (._;>;);
                             out body;
                             """;

        var response = await _httpClient.GetAsync($"{overpassUrl}/{overpassQuery}");

        return response;
    }
}