using LLDriver.Probes;
using System;
using System.Threading.Tasks;

namespace LLDriver.Cards
{
    public class UsbCard : IDevice
    {
        public void Close()
        {
            DeviceProbe.SendInternal(new WebHubDefinitonFWLib.TrafficMessages()
            {
                Internal = nameof(UsbCard) + " Closing"
            });
        }

        public void ExecuteTask(int tasknumber = 0)
        {
            DeviceProbe.SendInternal(new WebHubDefinitonFWLib.TrafficMessages()
            {
                Internal = nameof(UsbCard) + string.Format(" Task Number is {0}", tasknumber.ToString())
            });

            switch (tasknumber)
            {
                case 0:
                    Task.Delay(10).Wait();
                    break;
                case 1:
                    Task.Delay(100).Wait();
                    break;
                case 2:
                    Task.Delay(200).Wait();
                    break;
                case 3:
                    Task.Delay(300).Wait();
                    break;
                case 4:
                    Task.Delay(400).Wait();
                    break;
                case 5:
                    Task.Delay(5000).Wait();
                    break;
                default:
                    break;
            }
        }

        public void Init()
        {
            DeviceProbe.SendInternal(new WebHubDefinitonFWLib.TrafficMessages()
            {
                Internal = nameof(UsbCard) + " initializing"
            });
        }
    }
}
