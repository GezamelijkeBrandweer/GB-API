using GB_API;
using GB_API.Server.Application;
using GB_API.Server.Data;
using GB_API.Server.Data.IncidentDB;
using GB_API.Server.Data.LocatieDB;
using GB_API.Server.Domain;
using GB_API.Server.Domain.Traffic;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database connection/init
builder.Services.AddDbContext<MICDbContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("testbrandweer")));
builder.Services.AddScoped<IEntityRepository<Incident>, IncidentRepository>();
builder.Services.AddScoped<IEntityRepository<Locatie>, LocatieRepository>();
builder.Services.AddScoped<IEntityRepository<Karakteristiek>, KarakteristiekRepository>();
builder.Services.AddScoped<IEntityRepository<Meldingsclassificatie>, MeldingClassificatieRepository>();
builder.Services.AddConnections();

// Dependency injection
builder.Services.AddScoped<IIncidentService, IncidentService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Seed();
app.Run();