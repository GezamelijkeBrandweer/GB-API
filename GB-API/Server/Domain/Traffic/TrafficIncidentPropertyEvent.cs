namespace GB_API.Server.Domain.Traffic;

public class TrafficIncidentPropertyEvent
{
    public long Id { get; set; }
    
    public int code { get; set; }
    public string description { get; set; }
}