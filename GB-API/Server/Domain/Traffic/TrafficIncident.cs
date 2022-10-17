using System.ComponentModel.DataAnnotations.Schema;

namespace GB_API.Server.Domain.Traffic;

public class TrafficIncident
{
    public long Id { get; set; }
    public int IconCategory { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public double Length { get; set; }
    public int Delay { get; set; }
    public string[]? RoadNumbers { get; set; }

    // Voor nu niet nodig
    // [JsonPropertyName("numberOfReports")] 
    // public string? NumberOfReports { get; set; }
    public string? LastReportTime { get; set; }
    
    // Beschrijving van de trafficincident via meerdere events
    [NotMapped] public List<TrafficIncidentEvent> Events { get; } = new();
    
    // Custom mapper: Value Conversions
    // De coordinaten van de traffic incidenten
    [NotMapped] public List<GeoCoordinate> Coordinates { get; } = new();

}