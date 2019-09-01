using LLDriver.Probes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace LLDriver.Cards
{
    public class TimingCard : IDevice
    {
        private Timer timer;

        public void Close()
        {
            DeviceProbe.SendInternal(new WebHubDefinitonFWLib.TrafficMessages()
            {
                Internal = nameof(TimingCard) + " Closing"
            });
            timer.Stop();
        }

        public void ExecuteTask(int tasknumber = 0)
        {
            switch (tasknumber)
            {
                case 0:
                    ClockProbe.SendTime(DateTime.UtcNow);
                    //Task.Delay(10).Wait();
                    break;
                default:
                    break;
            }
        }

        public void Init()
        {
            DeviceProbe.SendInternal(new WebHubDefinitonFWLib.TrafficMessages()
            {
                Internal = nameof(TimingCard) + " initializing"
            });

            timer = new Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.ExecuteTask();
        }
    }
}
