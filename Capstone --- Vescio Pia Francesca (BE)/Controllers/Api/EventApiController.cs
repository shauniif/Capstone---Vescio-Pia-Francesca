using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EventApiController : ControllerBase
    {
        private readonly IEventService _eventSvc;

        public EventApiController(IEventService eventSvc)
        {
            _eventSvc = eventSvc;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> AllEvents()
            {
            var events = await _eventSvc.GetAll();
            return Ok(events);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
        {
            var currEvent = await _eventSvc.Read(id);
            return Ok(currEvent);
        }
    }
}
