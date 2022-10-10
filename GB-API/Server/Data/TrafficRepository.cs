using GB_API.Server.Domain.Traffic;

namespace GB_API.Server.Data;

public class TrafficRepository : IEntityRepository<TrafficIncident>
{
    private readonly MICDbContext _context;

    public TrafficRepository(MICDbContext context)
    {
        _context = context;
    }

    public List<TrafficIncident> FindAll()
    {
        return _context.TrafficIncidents.ToList();
    }

    public void Save(TrafficIncident t)
    {
        _context.Add(t);
        _context.SaveChanges();
    }

    public void DeleteById(long id)
    {
        var tIncident = _context.TrafficIncidents;
        var trafficIncident = GetById(id);
        if (trafficIncident == null) throw new NullReferenceException();
        tIncident.Remove(trafficIncident);
        _context.SaveChanges();
    }

    public TrafficIncident? GetById(long id)
    {
        return _context.TrafficIncidents.SingleOrDefault(trafficIncident => trafficIncident.Id == id);
    }
}