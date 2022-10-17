﻿using GB_API.Server.Data;
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
        var verkeersIncidents = Task.Run(() => _trafficService.GetTrafficIncidentsIn(""))
            .GetAwaiter().GetResult();
        
        // Hopelijk breekt dit niet het opslaan
        foreach (var verkeersIncident in verkeersIncidents)
        {
            incident.AddVerkeersIncident(verkeersIncident);
        }
        
        _incidentRepository.Save(incident);
        verkeersIncidents!.ForEach(vkIncident => incident.AddVerkeersIncident(vkIncident));
        
        return incident;
    }
}