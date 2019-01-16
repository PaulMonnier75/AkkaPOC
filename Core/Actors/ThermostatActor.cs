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

        public override void AroundPostRestart(Exception cause, object message)
        {
            base.AroundPostRestart(cause, message);

            Console.WriteLine("ThermostatActor PostRestart");
        }

        public override void AroundPostStop()
        {
            base.AroundPostStop();

            Console.WriteLine("ThermostatActor PostStop");
        }

        public override void AroundPreStart()
        {
            base.AroundPreStart();

            Console.WriteLine("ThermostatActor PreStart");
        }

        public override void AroundPreRestart(Exception cause, object message)
        {
            base.AroundPreRestart(cause, message);

            Console.WriteLine("ThermostatActor PreRestart");
        }

        private static void HandleSetTemperatureCommand(SetTemperatureCommand command)
        {
            var celsiusTemperature = ConvertFahrenheitToCelcius(command.FahrenheitTemperature);

            if (celsiusTemperature > 28)
                throw new ElectricityOverConsommationException();
            
            Console.WriteLine($"Call Thermostat API to set the temperature to {celsiusTemperature}°C");
        }

        private static double ConvertFahrenheitToCelcius(double fahrenheitTemperature)
        {
            try
            {
                return (fahrenheitTemperature - 32) * 5 / 9;
            }
            catch (Exception)
            {
                throw new TemperatureConvertionException();
            }
        }
    }
}