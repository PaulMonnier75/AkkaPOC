namespace Core.Models
{
    public abstract class Command
    {
        public CommandPriority Priority = CommandPriority.Medium;
    }      
    
    public abstract class MediaCommand : Command { }

    public abstract class HomeAutomationCommand : Command { }


    public class ChangeLightStatusCommand : HomeAutomationCommand
    {          
        public bool LightStatus { get; }

        public ChangeLightStatusCommand(bool status)
        {
            LightStatus = status;
        }
    }

    public class SetTemperatureCommand : HomeAutomationCommand
    {
        public double FahrenheitTemperature { get; }
        
        public SetTemperatureCommand(double fahrenheitTemperature)
        {
            FahrenheitTemperature = fahrenheitTemperature;
        }
    }
    
    public class PlayMovieCommand : MediaCommand
    {        
        public PlayMovieCommand()
        {
        }
    }
    
    public class PauseMovieCommand : MediaCommand
    {        
        public PauseMovieCommand()
        {
        }
    }

    public class CastMovieCommand : MediaCommand
    {
        public string UrlToCast { get; }

        public CastMovieCommand(string url)
            => UrlToCast = url;
    }
}