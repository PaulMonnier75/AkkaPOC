using System.Collections.Generic;
using Core.Models;

namespace Core.IAdapters.LeftSide
{
    public interface IHomeAutomationAdapter
    {
        void ChangeLightStatus(bool shouldBeOn);
        void SetTemperature(double fahrenheitTemperature);
    }
}