
using GB_API.Server.Domain;

namespace GB_API.Server.Data;

public class IncidentRepository : IEntityRepository<Incident>
{
    private readonly MICDbContext _context;

    public IncidentRepository(MICDbContext context)
    {
        _context = context;
    }

    public List<Incident> FindAll()
    {
        return _context.Incidents.ToList();
    }

    public void Save(Incident incident)
    {
        _context.Add(incident);
        _context.SaveChanges();
    }

    public void DeleteById(long id)
    {
        var incidents = _context.Incidents;
        var incidentToRemove = GetById(id);
        if (incidentToRemove == null) throw new NullReferenceException();
        incidents.Remove(incidentToRemove);
        _context.SaveChanges();
    }

    public Incident? GetById(long id)
    {
        return _context.Incidents.SingleOrDefault(incident => incident.Id == id);
    }
}