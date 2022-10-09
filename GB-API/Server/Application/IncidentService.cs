using GB_API.Server.Data;
using GB_API.Server.Data.IncidentDB;
using GB_API.Server.Domain;

namespace GB_API.Server.Application;

public class IncidentService : IIncidentService
{
    private readonly IEntityRepository<Incident> _entityRepository;

    public IncidentService(IEntityRepository<Incident> entityRepository)
    {
        _entityRepository = entityRepository;
    }

    public Incident Save(string name)
    {
        var incident = new Incident(name, new Locatie("Blaricum", "drop", 12, "d", 52.352562, 3.22524));
        _entityRepository.Save(incident);
        return incident;
    }
}