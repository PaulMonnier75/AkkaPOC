using System;
using Akka.Actor;
using Core.Models;

namespace Core.Actors
{
    public class MediaActor : ReceiveActor
    {    
        public MediaActor()
        {
            Receive<CastMovieCommand>(command => Console.WriteLine($"Call Chromecast API to cast the URL : {command.UrlToCast}"));
            Receive<PauseMovieCommand>(command => Console.WriteLine("Call Chromecast API to pause the movie"));
            Receive<PlayMovieCommand>(command => Console.WriteLine("Call Chromecast API to play the movie"));
            ReceiveAny(command => throw new Exception("MediaActor: Undefined Command"));
        }
    }
}