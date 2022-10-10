using GB_API.Server.Application;
using GB_API.Server.Domain;
using Microsoft.AspNetCore.Mvc;

namespace GB_API.Server.Presentation
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentService _service;

        public IncidentController(IIncidentService service)
        {
            _service = service;
        }

        [HttpPost]
        public Incident Save(string name, string niveau1, string niveau2, string niveau3, string afkorting, string presentatieTekst, string definitie, string naam, string type, int volgNr, string waarde )
        {
            return _service.Save(name, new MeldingsClassificaties(niveau1, niveau2, niveau3, afkorting,presentatieTekst, definitie), new Karakteristiek(naam, type, volgNr, waarde));
        }
    }
}
