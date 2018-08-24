using System.Collections.Generic;
using Akka.Actor;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Autofac;
using Core.Actors;
using Core.IAdapters.RightSide;
using Core.Models;
using Core.Services;
using Microsoft.Extensions.Options;

namespace Core
{
    public interface ICore
    {
        Event HandleCommand(Command command);
    }

    public class Core : ICore
    {        
        private readonly IActorRef CommandHandlerActorRef;
        
        public Core(IActorRefFactory actorSystem)
            => CommandHandlerActorRef = actorSystem.ActorOf(CommandHandlerActor.Props);

        public Event HandleCommand(Command command)
        {
            CommandHandlerActorRef.Tell(command);

            return new MessageRetrieved(new List<ChatMessage>());
        }
        
        public static void ConfigureIoc(ContainerBuilder builder)
        {
            builder.Register((c, p) => new ChatService(c.Resolve<IChatRepositoryAdapter>()))
                .As<IChatService>().SingleInstance();
            
            builder.Register((c, p) => new LuisService(c.Resolve<LuisSecrets>()))
                .As<ILuisService>().SingleInstance();

            builder.RegisterType<ChatActor>().SingleInstance();
        }

        public static void Resolver(IContainer container, ActorSystem actorSystem)
            => new AutoFacDependencyResolver(container, actorSystem);
     
    }
}