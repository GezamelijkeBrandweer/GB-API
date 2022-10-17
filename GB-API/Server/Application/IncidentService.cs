using GB_API.Server.Data;
using GB_API.Server.Domain;

namespace GB_API.Server.Application;

public class IncidentService : IIncidentService
{
    private readonly IEntityRepository<Incident> _incidentRepository;
    private readonly IEntityRepository<Karakteristiek> _karatkteristiekRepository;
    private readonly IEntityRepository<MeldingsClassificaties> _meldingRepository;
    private readonly TrafficService _trafficService;

    public IncidentService(IEntityRepository<Incident> incidentRepository, TrafficService trafficService, IEntityRepository<Karakteristiek> karatkteristiekRepository, IEntityRepository<MeldingsClassificaties> meldingRepository)
    {
        _incidentRepository = incidentRepository;
        _trafficService = trafficService;
        _karatkteristiekRepository = karatkteristiekRepository;
        _meldingRepository = meldingRepository;
    }

    public Incident Save(string name, long meldingId, long karakteristiekId)
    {
        var melding = _meldingRepository.GetById(meldingId);
        var karakteristiek = _karatkteristiekRepository.GetById(karakteristiekId);
        var incident = new Incident(name, melding, new Locatie("Blaricum", "drop", 12, "d", 52.352562, 3.22524));
        incident.AddKarkteristieken(karakteristiek);
        
        var verkeersIncidents = Task.Run(() => _trafficService.GetTrafficIncidentsIn("Erasmusbrug", kilometerRadius: 1.0))
            .GetAwaiter().GetResult();
        
        verkeersIncidents!.ForEach(vkIncident => incident.AddVerkeersIncident(vkIncident));
        
        _incidentRepository.Save(incident);
        return incident;
    }
}