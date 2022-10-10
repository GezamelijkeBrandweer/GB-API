using System.Text.Json.Serialization;
using GB_API.Server.Domain.VerkeersIncident;

namespace GB_API.Server.Domain.VerkeersIncident;

// Deserializer klasse
public class TrafficIncident
{
    public long Id { get; set; }
    [JsonPropertyName("properties")]
    public TrafficIncidentProperty Properties { get; set; }
    
    [JsonPropertyName("geometry")]
    public TrafficIncidentGeometry Geometry { get; set; }
}