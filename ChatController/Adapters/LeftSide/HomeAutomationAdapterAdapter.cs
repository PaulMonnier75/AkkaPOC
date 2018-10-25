using System.Collections.Generic;
using Core;
using Core.IAdapters.LeftSide;
using Core.Models;

namespace ChatController.Adapters.LeftSide
{
    public class HomeAutomationAdapterAdapter : IHomeAutomationAdapter
    {
        private readonly ICore Core;        
        
        public HomeAutomationAdapterAdapter(ICore core)
            => Core = core;

        public void ChangeLightStatus(bool shouldBeOn)
            => Core.HandleCommand(new ChangeLightStatusCommand(shouldBeOn));

        public void SetTemperature(double fahrenheitTemperature)
            => Core.HandleCommand(new SetTemperatureCommand(fahrenheitTemperature));
    }
}