using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Places_Service.Models;

namespace Places_Service.Services;

public class GooglePlaceService : IGooglePlaceService
{
    private readonly HttpClient _httpClient;

    public GooglePlaceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<PlacesApiResponse> GetPlaceByCoordinate(float lat, float lon, int radius = 1000)
    {
        var apiKey = "AIzaSyCmCZMRuxBtuJmFRJAuGKhobIExrksG4l0";
        var culture = CultureInfo.InvariantCulture;
        var latFormatted = lat.ToString(culture);
        var radiusFormatted = radius.ToString(culture);
        var lonFormatted = lon.ToString(culture);
        var url = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={latFormatted},{lonFormatted}&radius={radiusFormatted}&key={apiKey}";

        HttpResponseMessage response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var placesData = JsonConvert.DeserializeObject<PlacesApiResponse>(jsonResponse);
            if (placesData != null) return placesData;
        }

        return null!;
    }
}