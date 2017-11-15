using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDemoIoT
{
    public class Lights
    {
        public bool Switched { get; set; }
        public string Dimmer { get; set; }



        public void SwitchOff()
        {
            Switched = false;
            Dimmer = "OFF";
        }

        public void SwitchOn(int DimmerValue)
        {
            if (DimmerValue != 0)
            {
                Dimmer = DimmerValue.ToString();
            }
            else
                Dimmer = "ERROR";
        }
    }
}
