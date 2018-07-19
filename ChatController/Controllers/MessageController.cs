using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ChatController.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        [HttpGet]
        public IEnumerable<string> GetMessages()
        {
            return new string[] {"value1", "value2"};
        }

        [HttpPost]
        public void PostNewMessage([FromBody] string value)
        {
        }
    }
}