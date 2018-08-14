using System;
using Akka.Actor;
using Akka.DI.Core;
using Core.Models;
using Core.Services;

namespace Core.Actors
{
    public class CommandHandlerActor : ReceiveActor
    {
        public static Props Props => Props.Create(() => new CommandHandlerActor());
        
        public CommandHandlerActor()
        {
            Receive<ChatCommand>(command => DelegateChatCommandToChildActo(command));
            ReceiveAny(message => Console.WriteLine("UnknownCommand"));
        }

        private void DelegateChatCommandToChildActo(ChatCommand chatCommand)
        {
            //var a = CreateChatActorIfNotExist("ChatActor");
            var props = Context.DI().Props<ChatActor>();
            var a = Context.ActorOf(props, "chatActor");
            a.Tell(chatCommand);
        }

        private IActorRef CreateChatActorIfNotExist(string actorName)
            => !Context.Child(actorName).IsNobody() 
                    ? Context.Child(actorName) 
                    : Context.ActorOf(Context.DI().Props<ChatActor>(), actorName);
    }
}