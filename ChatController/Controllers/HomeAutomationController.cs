using Controller.Controllers.RequestParameterModels;
using Core.IAdapters.LeftSide;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    public class HomeAutomationController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IHomeAutomationAdapter HomeAutomationAdapter;

        public HomeAutomationController(IHomeAutomationAdapter homeAutomationAdapter)
        {
            HomeAutomationAdapter = homeAutomationAdapter;
        }

        [HttpPost("ChangeLightStatus")]
        public IActionResult SetLightStatus([FromBody] HomeAutomationRequestParameters parameter)
        {
            HomeAutomationAdapter.ChangeLightStatus(parameter.ShouldBeTurnedOn);

            return Ok();
        }

        [HttpPost("SetTemperature")]
        public IActionResult SetTemperature([FromBody] SetTemperatureRequestParameter parameter)
        {
            HomeAutomationAdapter.SetTemperature(parameter.FahrenheitTemperature);
            
            return Ok();
        }
    }
}