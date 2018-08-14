using ChatController.Adapters.LeftSide;
using Core.IAdapters.LeftSide;
using Microsoft.AspNetCore.Mvc;
using Plugins.Repositories;

namespace ChatController.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly IChatAdapter ChatAdapter;

        public MessageController(IChatAdapter chatAdapter)
            => ChatAdapter = chatAdapter;
        
        [HttpGet("all")]
        public IActionResult GetMessages()
        {
            var result = ChatAdapter.RetrieveMessages();
            
            return Ok(result);
        }

        [HttpPost]
        public void PostNewMessage([FromBody] string value)
        {
        }
    }
}