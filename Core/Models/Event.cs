﻿using System.Collections.Generic;
using System.Linq;

namespace Core.Models
{
    public abstract class Event { }

    public class MessageRetrieved : Event
    {
        public List<ChatMessage> Messages;

        public MessageRetrieved(IEnumerable<ChatMessage> messages)
            => Messages = messages.ToList();
    } 
}