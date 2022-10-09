using System.Text.Json.Serialization;

namespace GB_API.Server.Domain.VerkeersIncident;

public class TrafficIncidentProperty
{
    [JsonPropertyName("iconCategory")]
    public int IconCategory { get; set; }
    
    [JsonPropertyName("startTime")]
    public DateTime StartTime { get; set; }
    
    [JsonPropertyName("endTime")]
    public DateTime? EndTime { get; set; }
    
    [JsonPropertyName("from")]
    public string From { get; set; }
    
    [JsonPropertyName("to")]
    public string To { get; set; }
    
    [JsonPropertyName("length")]
    public double PropertyLength { get; set; }
    
    [JsonPropertyName("delay")]
    public int Delay { get; set; }
    
    [JsonPropertyName("roadNumbers")]
    public List<dynamic> RoadNumbers { get; set; } 
    
    [JsonPropertyName("timeValidity")]
    public string TimeValidity { get; set; }
    
    [JsonPropertyName("probabilityOfOccurrence")]
    public string ProbabilityOfOccurrence { get; set; }
    
    [JsonPropertyName("numberOfReports")]
    public object NumberOfReports { get; set; }
    
    [JsonPropertyName("lastReportTime")]
    public object LastReportTime { get; set; }

    [JsonPropertyName("events")]
    public List<dynamic> Events { get; set; } 
}