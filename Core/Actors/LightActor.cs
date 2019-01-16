using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Core.Models;

namespace Core.Actors
{
    public class LightActor : ReceiveActor
    {
        public LightActor()
        {
            Receive<ChangeLightStatusCommand>(command => HandleChangeLightStatusCommand(command));
            ReceiveAny(command => throw new Exception("LightActor : Undefined Command"));
        }

        public override void AroundPostRestart(Exception cause, object message)
        {
            base.AroundPostRestart(cause, message);

            Console.WriteLine("LightActor PostRestart");
        }

        public override void AroundPostStop()
        {
            base.AroundPostStop();

            Console.WriteLine("LightActor PostStop");
        }

        public override void AroundPreStart()
        {
            base.AroundPreStart();

            Console.WriteLine("LightActor PreStart");
        }

        public override void AroundPreRestart(Exception cause, object message)
        {
            base.AroundPreRestart(cause, message);

            Console.WriteLine("LightActor PreRestart");
        }

        private static void HandleChangeLightStatusCommand(ChangeLightStatusCommand command)
            => Console.WriteLine($"Changing light status to {command.LightStatus}");
    }
}
