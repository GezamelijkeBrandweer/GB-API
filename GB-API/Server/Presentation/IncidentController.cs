using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GB_API.Server.Application;
using GB_API.Server.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GB_API.Server.Presentation
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        private readonly TrafficService _trafficService;
        public IncidentController(TrafficService trafficService)
        {
            this._trafficService = trafficService;
        }
        // Voorstellen dat deze route wordt geroepen of iets dergelijks wanneer in het gmc een incident binnenkomt
        [HttpPost]
        public Incident? Create(long id, string? name, string postcode, int huisnummer)
        {
            var incident = new Incident(1L);
            
            var verkeersIncidents = Task.Run(() => _trafficService.GetTrafficIncidentsIn($"{postcode} {huisnummer}")).GetAwaiter().GetResult();
            foreach (var verkeersIncident in verkeersIncidents)
            {
                incident.AddVerkeersIncident(verkeersIncident);
            }
            return incident;
        }
    }
}
