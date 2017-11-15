using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SensorDemoIoT
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Sauna HouseSauna = new Sauna();
        public Lights LivingRoom = new Lights();
        public Lights Kitchen = new Lights();

        public MainPage()
        {
            this.InitializeComponent();
            txbLivingRoom.Text = "OFF";
            txbKitchen.Text = "OFF";

        }

        internal async void SetScreenText(string text)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                txtBlockHello.Text = text;
            });
        }

        internal async void SetScreenText1(string text)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                txbSauna.Text = text;
            });
        }

        internal async void SetScreenText2(string text)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                txbLivingRoom.Text = text;
            });
        }

        internal async void SetScreenText3(string text)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                txbKitchen.Text = text;
            });
        }

        private void btnHello_Click(object sender, RoutedEventArgs e)
        {
            txtBlockHello.Text = "Hei, Raspberry!";
        }
        private void valot_Click(object sender, RoutedEventArgs e)
        {
            RPi.SenseHat.Demo.DemoRunner.Run(s => new
                RPi.SenseHat.Demo.Demos.DiscoLights(s, this));
        }

        private void teksti_Click(object sender, RoutedEventArgs e)
        {
            RPi.SenseHat.Demo.DemoRunner.Run(s => new
                RPi.SenseHat.Demo.Demos.MultiColorScrollText(s, this,
                "Moikka Porvoo!"));
        }

        //private void sensorit_Click(object sender, RoutedEventArgs e)
        //{
        //    RPi.SenseHat.Demo.DemoRunner.Run(s => new
        //    RPi.SenseHat.Demo.Demos.ReadAllSensors(s, this, SetScreenText));
        //}

        private void btnSaunaTila_Click(object sender, RoutedEventArgs e)
        {

            //RPi.SenseHat.Demo.DemoRunner.Run(s => new
            //   RPi.SenseHat.Demo.Demos.MultiColorScrollText(s, this,
            //   "SAUNA HEAT OFF"));

            if (HouseSauna.Switched)
            {
                HouseSauna.SaunaOn(0);
                txbSauna.Text = "SAUNA HEAT OFF";
                //SaunaTimer.Stop();
                //SaunaOffTimer.Start();

            }
            else
            {
                HouseSauna.SaunaOn(1);
                txbSauna.Text = "SAUNA HEAT ON";
                //        //SaunaTimer.Start();

                //        //CommandManager.InvalidateRequerySuggested();

            }
        }

        private void btnOlohuoneValotila_Click(object sender, RoutedEventArgs e)
        {
            LivingRoom.SwitchOff();
            txbLivingRoom.Text = LivingRoom.Dimmer;
        }

        private void btnOlohuone33_Click(object sender, RoutedEventArgs e)
        {
            LivingRoom.SwitchOn(33);
            txbLivingRoom.Text = LivingRoom.Dimmer;

        }

        private void btnOlohuone66_Click(object sender, RoutedEventArgs e)
        {
            LivingRoom.SwitchOn(66);
            txbLivingRoom.Text = LivingRoom.Dimmer;
        }

        private void btnOlohuone100_Click(object sender, RoutedEventArgs e)
        {
            LivingRoom.SwitchOn(100);
            txbLivingRoom.Text = LivingRoom.Dimmer;
        }

        private void btnKitchenTila_Click(object sender, RoutedEventArgs e)
        {
            Kitchen.SwitchOff();
            txbKitchen.Text = Kitchen.Dimmer;
        }

        private void btnKitchen33_Click(object sender, RoutedEventArgs e)
        {
            Kitchen.SwitchOn(33);
            txbKitchen.Text = Kitchen.Dimmer;
        }

        private void btnKitchen66_Click(object sender, RoutedEventArgs e)
        {
            Kitchen.SwitchOn(66);
            txbKitchen.Text = Kitchen.Dimmer;
        }

        private void btnKitchen100_Click(object sender, RoutedEventArgs e)
        {
            Kitchen.SwitchOn(100);
            txbKitchen.Text = Kitchen.Dimmer;
        }

        //private void sldLivingRoom_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        //{
        //    txbLivingRoom.Text = sldLivingRoom.Value.ToString() + " %";
        //}
    }
}

