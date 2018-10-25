using System;
using Akka.Actor;
using Akka.DI.Core;
using Core.Models;

namespace Core.Actors
{
    public class CommandHandlerActor : ReceiveActor
    {
        public static Props Props => Props.Create(() => new CommandHandlerActor());

        public CommandHandlerActor()
        {
            Receive<HomeAutomationCommand>(command => DelegateHomeAutomationCommandToChildActor(command));
            Receive<MediaCommand>(command => DelegateMediaCommandToChildActor(command));
            ReceiveAny(message => Console.WriteLine("UnknownCommand"));
        }

        private static void DelegateHomeAutomationCommandToChildActor(HomeAutomationCommand homeAutomationCommand)
            => CreateHomeAutomationActorIfNotExist("HomeAutomation").Tell(homeAutomationCommand);
        
        private static void DelegateMediaCommandToChildActor(MediaCommand mediaCommand)
            => CreateMediaActorIfNotExist("MediaActor").Tell(mediaCommand);

        private static IActorRef CreateMediaActorIfNotExist(string actorName)
            => !Context.Child(actorName).IsNobody() 
                    ? Context.Child(actorName) 
                    : Context.ActorOf(Context.DI().Props<MediaActor>(), actorName);
        
        private static IActorRef CreateHomeAutomationActorIfNotExist(string actorName)
            => !Context.Child(actorName).IsNobody() 
                ? Context.Child(actorName) 
                : Context.ActorOf(Context.DI().Props<HomeAutomationActor>(), actorName);
    }
}