using System.Collections.Generic;
using Core.Models;

namespace Core.IAdapters.RightSide
{
    public interface IChatRepositoryAdapter
    {
        List<ChatMessage> GetAllMessages();
    }
}