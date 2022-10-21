using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapp_travel_agency.Data;
using webapp_travel_agency.Models;

namespace webapp_travel_agency.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageApiController : ControllerBase
    {
        ApplicationDbContext context;

        public MessageApiController()
        {
            context = new ApplicationDbContext();
        }

        [HttpPost]
        public IActionResult Send(Message message)
        {
            context.Messages.Add(message);
            context.SaveChanges();

            return Ok();
        }
    }
}
