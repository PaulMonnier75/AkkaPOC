using System;

namespace Core.Models
{
    public class ChatMessage
    {
        public string Text { get; set; }
        public string UserName { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}