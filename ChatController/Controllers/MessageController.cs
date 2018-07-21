using Core.IAdapters.LeftSide;
using Microsoft.AspNetCore.Mvc;

namespace ChatController.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly IChatAdapter ChatAdapter;
        
        public MessageController(IChatAdapter chatAdapter)
            => ChatAdapter = chatAdapter;
        
        [HttpGet("all")]
        public OkResult GetMessages()
        {
            ChatAdapter.RetriveMessage();
            
            return Ok();
        }

        [HttpPost]
        public void PostNewMessage([FromBody] string value)
        {
        }
    }
}