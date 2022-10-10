using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GB_API.Server.Domain.VerkeersIncident;

public class TrafficIncidentGeometry
{
    public long Id { get; set; }
    [JsonPropertyName("type")]
    private string Type { get; }
    
    [JsonPropertyName("coordinates")]
    [NotMapped]
    public List<List<double>> Coordinates { get; set; }
}