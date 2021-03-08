using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace AnotherBlog.CommentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly string port;

        public ValuesController(IConfiguration configuration)
        {
            port = configuration["port"];
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { $"value from Comment Api, Port:{port}, Date: {DateTime.Now.ToString("HH:mm:ss")}" };
        }
    }
}
