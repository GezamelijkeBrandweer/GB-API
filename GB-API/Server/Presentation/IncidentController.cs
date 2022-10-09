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
        public Incident Save(string name)
        {
            return _service.Save(name);
        }
    }
}
