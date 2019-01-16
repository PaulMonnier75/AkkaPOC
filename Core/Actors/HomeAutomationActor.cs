using System;
using Akka.Actor;
using Akka.DI.Core;
using Core.Models;

namespace Core.Actors
{
    public class HomeAutomationActor : ReceiveActor
    {
        public HomeAutomationActor()
        {
            Receive<ChangeLightStatusCommand>(command => DelegateChangeLightStatusCommandToChildActor(command));
            Receive<SetTemperatureCommand>(command => DelegateTemperatureCommandToChildActor(command));
            ReceiveAny(command => throw new Exception("ChatActor: Undefined Command"));
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(exception =>
            {
                if (exception is TemperatureConvertionException)
                    return Directive.Resume;

                if (exception is ElectricityOverConsommationException)
                    return Directive.Restart;

                return Directive.Stop;
            });
        }

        private static void DelegateTemperatureCommandToChildActor(SetTemperatureCommand temperatureCommand)
            => CreateThermostatActorIfNotExist("Thermostat").Tell(temperatureCommand);

        private static void DelegateChangeLightStatusCommandToChildActor(ChangeLightStatusCommand lightCommand)
            => CreateLightActorIfNotExist("Light").Tell(lightCommand);

        private static IActorRef CreateThermostatActorIfNotExist(string actorName)
            => !Context.Child(actorName).IsNobody()
                ? Context.Child(actorName)
                : Context.ActorOf(Context.DI().Props<ThermostatActor>(), actorName);

        private static IActorRef CreateLightActorIfNotExist(string actorName)
            => !Context.Child(actorName).IsNobody()
                ? Context.Child(actorName)
                : Context.ActorOf(Context.DI().Props<LightActor>(), actorName);
    }
}