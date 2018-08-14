using System;
using System.Collections.Generic;
using Core.IAdapters.RightSide;
using Core.Models;

namespace Core.Services
{
    public interface IChatService
    {
        List<ChatMessage> GetChatMessage();
    }
    
    public class ChatService : IChatService
    {
        private readonly IChatRepositoryAdapter ChatRepositoryAdapter;
        
        public ChatService(IChatRepositoryAdapter repositoryAdapter)
        {
            ChatRepositoryAdapter = ChatRepositoryAdapter;
        }

        public List<ChatMessage> GetChatMessage()
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