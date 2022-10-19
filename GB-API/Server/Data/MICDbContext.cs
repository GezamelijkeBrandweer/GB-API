using GB_API.Server.Domain;
using GB_API.Server.Domain.Traffic;
using Microsoft.EntityFrameworkCore;

namespace GB_API.Server.Data;

public class MICDbContext : DbContext
{
    public DbSet<Locatie> Locaties { get; set; }
    public DbSet<Incident> Incidents { get; set; }
    public DbSet<MeldingsClassificaties> MeldingsClassificaties { get; set; }
    public DbSet<Karakteristiek> Karakteristieks { get; set; }
    
    public DbSet<TrafficIncident> TrafficIncidents { get; set; }


    public MICDbContext(DbContextOptions<MICDbContext> options)
    : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Configure default schema
        modelBuilder.HasDefaultSchema("MIC_DB");
        
        //map entity to table
        modelBuilder.Entity<Incident>().ToTable("incident");
        modelBuilder.Entity<Karakteristiek>().ToTable("karakteriestiek");
        modelBuilder.Entity<MeldingsClassificaties>().ToTable("meldingClassificatie");
        modelBuilder.Entity<Locatie>().ToTable("locatie");
        modelBuilder.Entity<TrafficIncident>().ToTable("trafficIncident");
        
        //Configure relations
        modelBuilder.Entity<Incident>().HasMany(i => i.KarakteristiekList).WithMany("_incidents");
        modelBuilder.Entity<Incident>().HasMany(i => i.VerkeersIncidenten).WithMany("_incidents");
        modelBuilder.Entity<Incident>().HasOne(i => i.Locatie);
        modelBuilder.Entity<Incident>().HasOne(i => i.MeldingsClassificaties);
        


        modelBuilder.Entity<Incident>().Navigation(i => i.Locatie).AutoInclude();
        modelBuilder.Entity<Incident>().Navigation(i => i.KarakteristiekList).AutoInclude();
        modelBuilder.Entity<Incident>().Navigation(i => i.MeldingsClassificaties).AutoInclude();
        modelBuilder.Entity<Incident>().Navigation(i => i.VerkeersIncidenten).AutoInclude();
        modelBuilder.UseSerialColumns();
    }
}