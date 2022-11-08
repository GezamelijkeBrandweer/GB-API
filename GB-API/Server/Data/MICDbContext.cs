using GB_API.Server.Domain;
using GB_API.Server.Domain.Traffic;
using Microsoft.EntityFrameworkCore;

namespace GB_API.Server.Data;

public class MICDbContext : DbContext
{
    public DbSet<Locatie> Locaties { get; set; }
    public DbSet<Incident> Incidents { get; set; }
    public DbSet<Meldingsclassificatie> MeldingsClassificaties { get; set; }
    public DbSet<Karakteristiek> Karakteristieks { get; set; }

    public MICDbContext(DbContextOptions<MICDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Configure default schema
        modelBuilder.HasDefaultSchema("MIC-DB");

        //map entity to table
        modelBuilder.Entity<Incident>().ToTable("incident");
        modelBuilder.Entity<Karakteristiek>().ToTable("karakteriestiek");
        modelBuilder.Entity<Meldingsclassificatie>().ToTable("meldingClassificatie");
        modelBuilder.Entity<Locatie>().ToTable("locatie");

        //Configure relations
        modelBuilder.Entity<Incident>().HasMany(i => i.Karakteristieken).WithMany("_incidents");
        modelBuilder.Entity<Incident>().HasMany(i => i.Meldingsclassificaties).WithMany("_incidents");
        modelBuilder.Entity<Incident>().HasOne(i => i.Locatie);
        
        modelBuilder.UseSerialColumns();
    }
}