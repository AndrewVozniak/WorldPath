using System.Text;
using System.Text.Json;
using Routes.Models;

namespace Routes.Sync;

public class HttpDataClient : IPlacesDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    
    public HttpDataClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }
    
    public async Task<Place?> GetCoordinateFromPlaces(Coordinate coordinate)
    {
        var httpContent = new StringContent(
            JsonSerializer.Serialize(coordinate),
            Encoding.UTF8,
            "application/json"
        );

        var request = await _httpClient.
            GetAsync($"{_configuration["PlaceService"]}/{httpContent}");

        if (request.IsSuccessStatusCode)
        {
            Console.WriteLine("--> Sync POST to CommandService was OK!");
            Place? place = JsonSerializer.Deserialize<Place>(request.ToString());

            return place;
        }
        else
        {
            Console.WriteLine(request.StatusCode.ToString());
        }
        
        return null;
    }
}
