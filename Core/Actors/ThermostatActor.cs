using System;
using Akka.Actor;
using Core.Models;

namespace Core.Actors
{
    public class ThermostatActor : ReceiveActor
    {
        public ThermostatActor()
        {
            Receive<SetTemperatureCommand>(command => HandleSetTemperatureCommand(command));
            ReceiveAny(command => throw new Exception("ThermostatActor: Undefined Command"));      
        }

        private void HandleSetTemperatureCommand(SetTemperatureCommand command)
        {
            var celciusTemperature = ConvertFahrenheitToCelcius(command.FahrenheitTemperature);
            
            Console.WriteLine($"Call Thermostat API to set the tempearture to {celciusTemperature}°C");
        }

        private double ConvertFahrenheitToCelcius(double fahrenheitTemperature)
            => (fahrenheitTemperature - 32) * (5 / 9);
    }
}