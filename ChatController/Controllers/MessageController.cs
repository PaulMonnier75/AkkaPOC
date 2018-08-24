using System;
using Core.IAdapters.LeftSide;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ChatController.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly IChatAdapter ChatAdapter;

        public MessageController(IChatAdapter chatAdapter)
        {
            ChatAdapter = chatAdapter;
        }

        [HttpGet("all")]
        public IActionResult GetMessages()
        {
            var result = ChatAdapter.RetrieveMessages();

            return Ok(result);
        }

        [HttpPost]
        public void PostNewMessage([FromBody] ChatMessage chatMessage)
        {
            ChatAdapter.SendMessage(chatMessage);
        }
    }
}