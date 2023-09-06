using Newtonsoft.Json;

namespace Routes_Service.Dtos;

// public class RouteResponse
// {
//     public List<Route> Routes { get; set; }
//
//     public class Route
//     {
//         public List<Leg> Legs { get; set; }
//
//         public class Leg
//         {
//             public string Distance { get; set; }
//             public string Duration { get; set; }
//             public List<Step> Steps { get; set; }
//
//             public class Step
//             {
//                 public string Distance { get; set; }
//                 public string Duration { get; set; }
//                 public LatLng StartLocation { get; set; }
//                 public LatLng EndLocation { get; set; }
//                 
//                 public class LatLng
//                 {
//                     public float Lat { get; set; }
//                     public float Lng { get; set; }
//                 }
//             }
//         }
//     }
// }

public class RouteResponse
{
    public List<Route> Routes { get; set; }
}

public class Route
{
    public List<Leg> Legs { get; set; }
    
    [JsonProperty("overview_polyline")]
    public OverviewPolyline OverviewPolyline { get; set; }
}

public class Leg
{
    public Distance Distance { get; set; }
    // Додайте інші властивості за необхідності
}

public class Distance
{
    public string Text { get; set; }
    public int Value { get; set; }
}

public class OverviewPolyline
{
    public string Points { get; set; }
}
