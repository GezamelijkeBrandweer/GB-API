using GB_API.Server.Data;
using GB_API.Server.Data.IncidentDB;
using GB_API.Server.Domain;

namespace GB_API.Server.Application;

public class IncidentService : IIncidentService
{
    private readonly IEntityRepository<Incident> _entityRepository;
    private readonly TrafficService _trafficService;

    public IncidentService(IEntityRepository<Incident> entityRepository, TrafficService trafficService)
    {
        _entityRepository = entityRepository;
        _trafficService = trafficService;
    }

    public Incident Save(string name)
    {
        var incident = new Incident(name, new Locatie("Blaricum", "drop", 12, "d", 52.352562, 3.22524));
        
        var verkeersIncidents = Task.Run(() => _trafficService.GetTrafficIncidentsIn(""))
            .GetAwaiter().GetResult();
        
        // Hopelijk breekt dit niet het opslaan
        foreach (var verkeersIncident in verkeersIncidents)
        {
            incident.AddVerkeersIncident(verkeersIncident);
        }
        
        _entityRepository.Save(incident);
        return incident;
    }
}