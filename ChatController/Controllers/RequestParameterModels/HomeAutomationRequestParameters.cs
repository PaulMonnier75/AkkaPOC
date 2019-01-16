namespace Controller.Controllers.RequestParameterModels
{
    public class HomeAutomationRequestParameters
    {
        public bool ShouldBeTurnedOn { get; set; }
    }

    public class SetTemperatureRequestParameter
    {
        public double FahrenheitTemperature { get; set; }
    }
}