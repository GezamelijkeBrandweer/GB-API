using GB_API.Server.Domain;

namespace GB_API.Server.Application;

public interface IIncidentService
{
    Incident Save(string name, long meldingId, long karakteristiekId);
}