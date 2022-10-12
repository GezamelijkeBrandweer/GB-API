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
    
    //TODO Gebruik dit bij het inladen om te filteren op "probable en certain"
    // [JsonPropertyName("probabilityOfOccurrence")]
    // public string ProbabilityOfOccurrence { get; set; }

    // Voor nu niet nodig
    // [JsonPropertyName("numberOfReports")] 
    // public string? NumberOfReports { get; set; }
    public string? LastReportTime { get; set; }
    
    // [NotMapped]
    // Beschrijving van de trafficincident via meerdere events
    //public List<TrafficIncidentEvent> Events { get; set; } 
    
    // Custom mapper: Value Conversions
    // De coordinaten van de traffic incidenten
    //public List<GeoCoordinate> Coordinates { get; set; }
    
}