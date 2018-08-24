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
            ChatRepositoryAdapter = repositoryAdapter;
        }

        public List<ChatMessage> GetChatMessage()
        {
            return ChatRepositoryAdapter.GetAllMessages();            
        }       
    }
}