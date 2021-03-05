using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AnotherBlog.GatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value from Ocelot" };
        }
    }
}
