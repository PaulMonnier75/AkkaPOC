using System.Collections.Generic;
using Core.IAdapters.RightSide;
using Core.Models;

namespace Plugins.Repositories
{ 
    public class ChatRepository : IChatRepositoryAdapter
    {
        public List<ChatMessage> GetAllMessages()
        {
            throw new System.NotImplementedException();
        }
    }
}