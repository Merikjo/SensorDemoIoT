using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDemoIoT
{
    public class Sauna
    {
        public bool Switched { get; set; }
        public string Color { get; set; }

        public double lblLampotila { get; set; }

        public double SaunaTemperature { get; set; }


        public void SaunaOn(int tila)
        {
            if (tila == 0)
            {
                Switched = false;
                SaunaTemperature = 20.01;

            }
            else
            {
                Switched = true;

            }


        }
    }
}
