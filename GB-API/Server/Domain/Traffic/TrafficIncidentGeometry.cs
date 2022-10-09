using System.Text.Json.Serialization;

namespace GB_API.Server.Domain.VerkeersIncident;

public class TrafficIncidentGeometry
{
    [JsonPropertyName("type")]
    private string Type { get; }
    
    [JsonPropertyName("coordinates")]
    public List<List<double>> Coordinates { get; set; }
}