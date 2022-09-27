using GB_API.Server.Domain;
using Microsoft.EntityFrameworkCore;

namespace GB_API.Server.Data.IncidentDB;

public class IncidentContext : DbContext
{
    public IncidentContext(DbContextOptions<IncidentContext> options) : base(options)
    {
    }

    public DbSet<Incident> Incidents { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Incident>().Navigation(incident => incident.Locatie).AutoInclude();
    }
}