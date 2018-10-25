using Core.IAdapters.LeftSide;
using Microsoft.AspNetCore.Mvc;

namespace ChatController.Controllers
{
    [Route("api/[controller]")]
    public class HomeAutomationController : Controller
    {
        private readonly IHomeAutomationAdapter HomeAutomationAdapter;

        public HomeAutomationController(IHomeAutomationAdapter homeAutomationAdapter)
        {
            HomeAutomationAdapter = homeAutomationAdapter;
        }

        [HttpPost("ChangeLightStatus")]
        public IActionResult SetLightStatus([FromBody] bool shouldBeTurnedOn)
        {
            HomeAutomationAdapter.ChangeLightStatus(shouldBeTurnedOn);

            return Ok();
        }

        [HttpPost("SetTemperature")]
        public IActionResult SetTemperature([FromBody] double fahrenheitTemperature)
        {
            HomeAutomationAdapter.SetTemperature(fahrenheitTemperature);
            
            return Ok();
        }
    }
}