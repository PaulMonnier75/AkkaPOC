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
            Receive<ChangeLightStatusCommand>(command => Console.WriteLine("Change Light Status"));
            Receive<SetTemperatureCommand>(command => DelegateTemperatureCommandToChildActor(command));
            ReceiveAny(command => throw new Exception("ChatActor: Undefined Command"));
        }
        
        private static void DelegateTemperatureCommandToChildActor(SetTemperatureCommand temperatureCommand)
            => CreateThermostatActorIfNotExist("Thermostat").Tell(temperatureCommand);
        
        private static IActorRef CreateThermostatActorIfNotExist(string actorName)
            => !Context.Child(actorName).IsNobody() 
                ? Context.Child(actorName) 
                : Context.ActorOf(Context.DI().Props<ThermostatActor>(), actorName);
    }
}