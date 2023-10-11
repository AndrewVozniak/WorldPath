using Newtonsoft.Json;

namespace Places.Application.Dtos;

public class Photo
{
    public int Height { get; set; }
    public List<string> HtmlAttributions { get; set; }
    
    [JsonProperty("photo_reference")]
    public string PhotoReference { get; set; }

    public int Width { get; set; }
}