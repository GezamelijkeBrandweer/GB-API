using GB_API.Server.Domain;
using GB_API.Server.Domain.VerkeersIncident;
using Microsoft.EntityFrameworkCore;

namespace GB_API.Server.Data;

public class MICDbContext : DbContext
{
    public DbSet<Locatie> Locaties { get; set; }
    public DbSet<Incident> Incidents { get; set; }
    public DbSet<MeldingsClassificaties> MeldingsClassificaties { get; set; }
    public DbSet<Karakteristiek> Karakteristieks { get; set; }


    public MICDbContext(DbContextOptions<MICDbContext> options)
    : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Incident>().Navigation(i => i.Locatie).AutoInclude();
        modelBuilder.Entity<Incident>().Navigation(i => i.KarakteristiekList).AutoInclude();
        modelBuilder.Entity<Incident>().Navigation(i => i.MeldingsClassificaties).AutoInclude();
        modelBuilder.UseSerialColumns();
    }
}