using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Gpio;
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
using Windows.Web.Http;


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

        // Use GPIO pin 5 to set values
        private const int SET_PIN = 5;
        private GpioPin setPin;

        // Use GPIO pin 6 to listen for value changes
        private const int LISTEN_PIN = 6;
        private GpioPin listenPin;

        private GpioPinValue currentValue = GpioPinValue.High;
        private DispatcherTimer timer;
        private DispatcherTimer remoteCommandtimer;

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

        internal async void SetScreenText4(string text)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                txtBlockHello.Text = text;
            });
        }

        private void btnHello_Click(object sender, RoutedEventArgs e)
        {
            txtBlockHello.Text = "Hei, Älytalo";
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

        private void btnJoystick_Click(object sender, RoutedEventArgs e)
        {
            RPi.SenseHat.Demo.DemoRunner.Run(s => new
             RPi.SenseHat.Demo.Demos.JoystickPixel(s, this));
        }


        //private void sensorit_Click(object sender, RoutedEventArgs e)
        //{
        //    RPi.SenseHat.Demo.DemoRunner.Run(s => new
        //    RPi.SenseHat.Demo.Demos.ReadAllSensors(s, this, SetScreenText));
        //}

        private void btnSaunaTila_Click(object sender, RoutedEventArgs e)
        {
            if (HouseSauna.Switched)
            {
                HouseSauna.SaunaOn(0);
                txbSauna.Text = "SAUNA HEAT OFF";
                //SaunaTimer.Stop();
                //SaunaOffTimer.Start();

                //RPi.SenseHat.Demo.DemoRunner.Run(s => new
                //   RPi.SenseHat.Demo.Demos.MultiColorScrollText(s, this,
                //   "SAUNA OFF"));
            }
            else
            {
                HouseSauna.SaunaOn(1);
                txbSauna.Text = "SAUNA HEAT ON";
                //SaunaTimer.Start();

                //CommandManager.InvalidateRequerySuggested();
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

        private void btnTemperature_Click(object sender, RoutedEventArgs e)
        {
            RPi.SenseHat.Demo.DemoRunner.Run(s => new
              RPi.SenseHat.Demo.Demos.WriteTemperature(s, this));
        }

        private void btnGravity_Click(object sender, RoutedEventArgs e)
        {
            RPi.SenseHat.Demo.DemoRunner.Run(s => new
             RPi.SenseHat.Demo.Demos.GravityBlob(s, this));
        }

        //private void sldLivingRoom_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        //{
        //    txbLivingRoom.Text = sldLivingRoom.Value.ToString() + " %";
        //}


        void StartScenario()
        {
            // Initialize the GPIO objects.
            var gpio = GpioController.GetDefault();

            // Set up our GPIO pin for setting values.
            // If this next line crashes with a NullReferenceException,
            // then the problem is that there is no GPIO controller on the device.
            setPin = gpio.OpenPin(SET_PIN);

            // Establish initial value and configure pin for output.
            setPin.Write(currentValue);
            setPin.SetDriveMode(GpioPinDriveMode.Output);

            // Set up our GPIO pin for listening for value changes.
            listenPin = gpio.OpenPin(LISTEN_PIN);

            // Configure pin for input and add ValueChanged listener.
            listenPin.SetDriveMode(GpioPinDriveMode.Input);
            listenPin.ValueChanged += Pin_ValueChanged;

            // Start toggling the pin value every 500ms.
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            timer.Start();

            // Start toggling the pin value every 10000ms.
            remoteCommandtimer = new DispatcherTimer();
            remoteCommandtimer.Interval = TimeSpan.FromSeconds(10);
            remoteCommandtimer.Tick += getRemoteCommand;
            remoteCommandtimer.Start();
        }

        void StopScenario()
        {
            // Stop the timer.
            if (timer != null)
            {
                timer.Stop();
                timer = null;
            }

            // Release the GPIO pins.
            if (setPin != null)
            {
                setPin.Dispose();
                setPin = null;
            }
            if (listenPin != null)
            {
                listenPin.ValueChanged -= Pin_ValueChanged;
                listenPin.Dispose();
                listenPin = null;
            }
        }

        void StartStopScenario()
        {
            if (timer != null)
            {
                StopScenario();
                StartStopButton.Content = "Start";
                ScenarioControls.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                StartScenario();
                StartStopButton.Content = "Stop";
                ScenarioControls.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }
        private void Pin_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs e)
        {
            // Report the change in pin value.
            var task = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                CurrentPinValue.Text = listenPin.Read().ToString();
            });
        }

        private void Timer_Tick(object sender, object e)
        {
            // Toggle the existing pin value.
            currentValue = (currentValue == GpioPinValue.High) ? GpioPinValue.Low : GpioPinValue.High;
            setPin.Write(currentValue);
        }

        private async void getRemoteCommand(object sender, object e)
        {
            //var uri = new Uri("http://example.com/datalist.aspx");

            var uri = new Uri("http://localhost:37413/command/getAvailable/");//localhost
            var httpClient = new HttpClient();
         
            // Always catch network exceptions for async methods
            try
            {
                var result = await httpClient.GetStringAsync(uri);
                if (result != "")
                {
                    if (result == "SaunaOn")
                    {
                        btnSaunaTila_Click(null, null);
                    }
                }
            }
            catch
            {
                // Details in ex.Message and ex.HResult.       
            }

        }
    }
}

