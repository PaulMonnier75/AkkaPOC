using System.Collections.Generic;
using Akka.Actor;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Autofac;
using Core.Actors;
using Core.IAdapters.RightSide;
using Core.Models;
using Core.Services;

namespace Core
{
    public interface ICore
    {
        Event HandleCommand(Command command);
    }

    public class Core : ICore
    {
        private readonly IActorRef CommandHandlerActorRef;
        private readonly IChatRepositoryAdapter ChatRepositoryAdapter;

        public Core(IChatRepositoryAdapter chatRepoAdapter)
        {
            ChatRepositoryAdapter = chatRepoAdapter;

            var container = ConfigureDependyInjection();

            var actorSystemReference = ActorSystem.Create("ActorSystem");

            var resolver = new AutoFacDependencyResolver(container, actorSystemReference);

            CommandHandlerActorRef = actorSystemReference.ActorOf(CommandHandlerActor.Props);
        }

        public Event HandleCommand(Command command)
        {
            CommandHandlerActorRef.Tell(command);

            return new MessageRetrieved(new List<ChatMessage>());
        }

        private IContainer ConfigureDependyInjection()
        {       
            var builder = new ContainerBuilder();

            builder.Register((c, p) => new ChatService(ChatRepositoryAdapter)).As<IChatService>().SingleInstance();
            builder.RegisterType<ChatActor>();

            return builder.Build();
        }
    }
}