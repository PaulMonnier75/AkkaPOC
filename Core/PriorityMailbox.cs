using Akka.Actor;
using Akka.Configuration;
using Akka.Dispatch;
using Core.Models;

namespace Core
{
    public class PriorityMailbox : UnboundedPriorityMailbox
    {       
        // Non Implementé pour le moment
        public PriorityMailbox(Settings settings, Config config) : base(settings, config)
        {
        }

        protected override int PriorityGenerator(object message)
        {
            if (message is Command command)
                return (int) command.Priority;
            
            return (int) CommandPriority.Medium;
        }
    }
}