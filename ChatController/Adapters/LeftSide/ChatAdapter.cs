using System.Collections.Generic;
using Core;
using Core.IAdapters.LeftSide;
using Core.Models;

namespace ChatController.Adapters.LeftSide
{
    public class ChatAdapter : IChatAdapter
    {
        private readonly ICore Core;        
        
        public ChatAdapter(ICore core)
            => Core = core;

        public IEnumerable<ChatMessage> RetrieveMessages()
            => (Core.HandleCommand(new RetrieveMessageCommand()) as MessageRetrieved)?.Messages;

        public void SendMessage(ChatMessage msg)
        {
            throw new System.NotImplementedException();
        }
    }
}