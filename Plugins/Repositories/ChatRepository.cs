using System;
using System.Collections.Generic;
using Core.IAdapters.RightSide;
using Core.Models;

namespace Plugins.Repositories
{
    public class ChatRepository : IChatRepositoryAdapter
    {
        public List<ChatMessage> GetAllMessages()
        {
            return new List<ChatMessage>()
            {
                new ChatMessage()
                {
                    Text = "TMP",
                    UserName = "toto",
                    TimeStamp = DateTimeOffset.UtcNow
                }
            };
        }
    }
}