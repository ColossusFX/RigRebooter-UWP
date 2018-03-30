// Copyright (c) Microsoft. All rights reserved.

using Blinky.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Blinky
{
    public sealed partial class MainPage : Page
    {

        private GpioPin pin;
        private List<GpioPin> GpioList;
        private const GpioPinValue pinLow = GpioPinValue.Low;
        private const GpioPinValue pinHigh = GpioPinValue.High;
        private DispatcherTimer timer;

        private ObservableCollection<MiningRigs> MiningRigs;

        public MainPage()
        {
            InitializeComponent();

            GpioList = new List<GpioPin>();

            MiningRigs = new ObservableCollection<MiningRigs>();

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(10000)
            };

            timer.Tick += Timer_Tick;

            MiningRigs.Add(new MiningRigs("192.168.0.24", 12));
            MiningRigs.Add(new MiningRigs("192.168.0.40", 16));
            MiningRigs.Add(new MiningRigs("192.168.0.47", 20));
            MiningRigs.Add(new MiningRigs("192.168.0.119", 21));
            MiningRigs.Add(new MiningRigs("192.168.0.246", 26));
            MiningRigs.Add(new MiningRigs("192.168.0.251", 13));

            foreach (var rig in MiningRigs)
            {
                var col = String.Format("IP {0} Pin {1}", rig.IP, rig.PinNumber);

                Debug.WriteLine(col);

                InitGPIO(rig.PinNumber);

                if (!PingHost(rig.IP, 3333, 200))
                {
                    ResetPin(rig.IP, rig.PinNumber);
                }
            }

            if (pin != null)
            {
                timer.Start();
            }
        }

        private void InitGPIO(int pinNo)
        {
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                pin = null;
                GpioStatus.Text = "There is no GPIO controller on this device.";
                return;
            }

            pin = gpio.OpenPin(pinNo);

            GpioList.Add(pin);

            foreach (var outputPin in GpioList)
            {
                if (outputPin.PinNumber == outputPin.PinNumber)
                {
                    outputPin.Write(pinHigh);

                    outputPin.SetDriveMode(GpioPinDriveMode.Output);
                }
            }

            GpioStatus.Text = "GPIO pin initialized correctly.";
        }

        private void Timer_Tick(object sender, object e)
        {
            foreach (var rig in MiningRigs)
            {
                if (!PingHost(rig.IP, 3333, 100))
                {
                    ResetPin(rig.IP, rig.PinNumber);
                }
            }
        }

        // Ping port to check for claymore
        private bool PingHost(string host, int port, int timeout)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    var result = client.ConnectAsync(host, port);

                    var success = result.Wait(timeout);

                    if (!success)
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void ResetPin(string IP, int pinNo)
        {
            foreach (var pinNumber in GpioList)
            {
                if (pinNumber.PinNumber == pinNo)
                {
                    Debug.WriteLine("Pin " + pinNumber.PinNumber);

                    Debug.WriteLine("CLient " + IP + " offline, now rebooting... ");

                    pinNumber.Write(pinLow);

                    Task.Delay(2000).Wait();

                    pinNumber.Write(pinHigh);
                }
            }
        }
    }
}


