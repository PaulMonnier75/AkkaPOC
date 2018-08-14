namespace Core.Models
{
    public abstract class Command
    {
        public CommandPriority Priority = CommandPriority.Medium;
    }
    
    public abstract class ChatCommand : Command { }

    public class RetrieveMessageCommand : ChatCommand
    {        
        public RetrieveMessageCommand() { }
    }

    public class SendMessageCommand : ChatCommand
    {
        public ChatMessage Message { get; set; }
        
        public SendMessageCommand(ChatMessage message)
        {
            Message = message;
        }
    }
}