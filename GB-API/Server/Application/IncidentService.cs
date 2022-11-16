using GB_API.Server.Data;
using GB_API.Server.Domain;

namespace GB_API.Server.Application;

public class IncidentService : IIncidentService
{
    private readonly IEntityRepository<Incident> _incidentRepository;
    private readonly IEntityRepository<Karakteristiek> _karatkteristiekRepository;
    private readonly IEntityRepository<Meldingsclassificatie> _meldingRepository;
    private readonly IExtendedEntityRepository<Dienst> _dienstRepository;
    private readonly ReadIntensiteitenRepository _intensiteitenRepository;

    public IncidentService(IEntityRepository<Incident> incidentRepository, IEntityRepository<Karakteristiek> karatkteristiekRepository, 
        IEntityRepository<Meldingsclassificatie> meldingRepository, IExtendedEntityRepository<Dienst> dienstRepository, ReadIntensiteitenRepository intensiteitenRepository)
    {
        _incidentRepository = incidentRepository;
        _karatkteristiekRepository = karatkteristiekRepository;
        _meldingRepository = meldingRepository;
        _dienstRepository = dienstRepository;
        _intensiteitenRepository = intensiteitenRepository;
    }

    public Incident Save(string name, long meldingId, long karakteristiekId)
    {
        var melding = _meldingRepository.GetById(meldingId);
        var karakteristiek = _karatkteristiekRepository.GetById(karakteristiekId);

        var locatie = new Locatie("Hoekseweg", "3723EA", "33A", 52.352562, 3.22524);
        
        var incident = new Incident(name, locatie, melding);
        
        incident.Karakteristieken.Add(karakteristiek!);
        incident.Meldingsclassificatie = melding!;

        // Via een custom Repository worden nu alleen de nodige intensiteiten opgehaald
        Dienst dienst = _dienstRepository.GetByName("Brandweer")!;
        var classificatieIntensiteiten = _intensiteitenRepository
            .GetClassificatieIntensiteitenFromDienstAndClassificatie(dienst, incident.Meldingsclassificatie);
        classificatieIntensiteiten!.ForEach(c => dienst.MeldingsclassificatieIntensiteiten.Add(c));

        var karakteristiekIntensiteiten = _intensiteitenRepository
            .GetKarakteristiekIntensiteitenFromDienstAndKarakteristieken(dienst, incident.Karakteristieken.ToList());
        karakteristiekIntensiteiten!.ForEach(k => dienst.KarakteristiekIntensiteiten.Add(k));
        
        incident.AddIntensiteit(dienst);

        _incidentRepository.Save(incident);
        return incident;
    }  
}