using Akka.Actor;
using Akka.DI.AutoFac;
using Autofac;
using Core.Actors;
using Core.Models;

namespace Core
{
    public interface ICore
    {
        void HandleCommand(Command command);
    }

    public class Core : ICore
    {        
        private readonly IActorRef CommandHandlerActorRef;
        
        public Core(IActorRefFactory actorSystem)
            => CommandHandlerActorRef = actorSystem.ActorOf(CommandHandlerActor.Props);

        public void HandleCommand(Command command)
            => CommandHandlerActorRef.Tell(command);
        
        public static void ConfigureIoc(ContainerBuilder builder)
        {
            builder.RegisterType<HomeAutomationActor>().SingleInstance();
            builder.RegisterType<ThermostatActor>().SingleInstance();
            builder.RegisterType<MediaActor>().SingleInstance();
            builder.RegisterType<LightActor>().SingleInstance();
        }

        public static void Resolver(IContainer container, ActorSystem actorSystem)
            => new AutoFacDependencyResolver(container, actorSystem);   
    }
}