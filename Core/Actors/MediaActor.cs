using System;
using Akka.Actor;
using Core.Models;

namespace Core.Actors
{
    public class MediaActor : ReceiveActor
    {
        private bool IsMediaPaused;

        public MediaActor()
        {
            Receive<PlayMovieCommand>(command => PlayMovie());
            Receive<PauseMovieCommand>(command => PauseMovie());
            Receive<CastMovieCommand>(command => CastMovie(command.UrlToCast));
            ReceiveAny(command => throw new Exception("MediaActor: Undefined Command"));
        }

        private void SetMediaPlayerStatus(bool isMediaPaused)
            => IsMediaPaused = isMediaPaused;

        private void PlayMovie()
        {
            SetMediaPlayerStatus(false);
            if (IsMediaPaused)
                Console.WriteLine("Call Chromecast API to play the movie");
            else
                Console.WriteLine("Media already played");
        }

        private void PauseMovie()
        {
            SetMediaPlayerStatus(true);
            SendMessageToLoggerActor();
            if (IsMediaPaused)
                Console.WriteLine("Media alreday Paused");
            else
                Console.WriteLine("Call Chromecast API to pause the movie");
        }

        private void CastMovie(string urlToCast)
            => Console.WriteLine($"Call Chromecast API to cast the URL : {urlToCast}");

        private void SendMessageToLoggerActor()
        {
            try
            {
                Context.ActorSelection("akka://HomeActorSystem/user/HomeAutomationActor").Tell(new ChangeLightStatusCommand(true));

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}