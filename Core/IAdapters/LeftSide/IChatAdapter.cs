using System.Collections.Generic;
using Core.Models;

namespace Core.IAdapters.LeftSide
{
    public interface IChatAdapter
    {
        IEnumerable<ChatMessage> RetrieveMessages();
        void SendMessage(ChatMessage msg);
    }
}