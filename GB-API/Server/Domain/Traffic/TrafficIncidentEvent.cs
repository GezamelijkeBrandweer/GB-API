namespace GB_API.Server.Domain.Traffic;

public class TrafficIncidentEvent
{
    public long Id { get; set; }
    
    public int Code { get; }
    public string Description { get; }

    public TrafficIncidentEvent(int code, string description)
    {
        Code = code;
        Description = description;
    }
}