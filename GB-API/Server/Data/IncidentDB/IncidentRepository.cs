
using GB_API.Server.Domain;

namespace GB_API.Server.Data.IncidentDB;

public class IncidentRepository : IEntityRepository<Incident>
{
    private readonly IncidentContext _context;

    public IncidentRepository(IncidentContext context)
    {
        _context = context;
    }

    public List<Incident> FindAll()
    {
        return _context.Incidents.ToList();
    }

    public void Save(Incident entity)
    {
        _context.Add(entity);
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
