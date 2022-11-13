using GB_API.Server.Data;
using GB_API.Server.Domain;

namespace GB_API.Server.Application;

public class IncidentService : IIncidentService
{
    private readonly IEntityRepository<Incident> _incidentRepository;
    private readonly IEntityRepository<Karakteristiek> _karatkteristiekRepository;
    private readonly IEntityRepository<Meldingsclassificatie> _meldingRepository;
    private readonly IExtendedEntityRepository<Dienst> _dienstRepository;

    public IncidentService(IEntityRepository<Incident> incidentRepository, IEntityRepository<Karakteristiek> karatkteristiekRepository, 
        IEntityRepository<Meldingsclassificatie> meldingRepository, IExtendedEntityRepository<Dienst> dienstRepository)
    {
        _incidentRepository = incidentRepository;
        _karatkteristiekRepository = karatkteristiekRepository;
        _meldingRepository = meldingRepository;
        _dienstRepository = dienstRepository;
    }

    public Incident Save(string name, long meldingId, long karakteristiekId)
    {
        var melding = _meldingRepository.GetById(meldingId);
        var karakteristiek = _karatkteristiekRepository.GetById(karakteristiekId);

        var locatie = new Locatie("Hoekseweg", "3723EA", "33A", 52.352562, 3.22524);
        
        var incident = new Incident(name, locatie, melding);
        
        incident.Karakteristieken.Add(karakteristiek!);
        incident.Meldingsclassificatie = melding!;

        // Via eagerly loading worden alle bijhorende KarakteristiekenIntensiteiten en MeldingClassificatiesIntensiteiten opgehaald
        // Dit duurt zelf al lang
        Dienst dienst = _dienstRepository.GetByName("Brandweer")!;
        incident.AddIntensiteit(dienst);

        _incidentRepository.Save(incident);
        return incident;
    }  
}