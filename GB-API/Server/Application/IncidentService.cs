using GB_API.Server.Data;
using GB_API.Server.Domain;

namespace GB_API.Server.Application;

public class IncidentService : IIncidentService
{
    private readonly IEntityRepository<Incident> _incidentRepository;
    private readonly IEntityRepository<Karakteristiek> _karatkteristiekRepository;
    private readonly IEntityRepository<Meldingsclassificatie> _meldingRepository;

    public IncidentService(IEntityRepository<Incident> incidentRepository, IEntityRepository<Karakteristiek> karatkteristiekRepository, IEntityRepository<Meldingsclassificatie> meldingRepository)
    {
        _incidentRepository = incidentRepository;
        _karatkteristiekRepository = karatkteristiekRepository;
        _meldingRepository = meldingRepository;
    }

    public Incident Save(string name, long meldingId, long karakteristiekId)
    {
        var melding = _meldingRepository.GetById(meldingId);
        var karakteristiek = _karatkteristiekRepository.GetById(karakteristiekId);

        var locatie = new Locatie("Hoekseweg", "3723EA", "33A", 52.352562, 3.22524);
        
        var incident = new Incident(name, locatie);
        
        incident.Karakteristieken.Add(karakteristiek!);
        incident.Meldingsclassificaties.Add(melding!);
        incident.Intensiteit = new Intensiteit(50, new Dienst("Brandweer"));

        _incidentRepository.Save(incident);
        return incident;
    }
}