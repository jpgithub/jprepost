using LLDriver.Cards;
using LLDriver.Probes;
using System;
using WebHubDefinitonFWLib;

namespace HLDriver
{
    public static class DeviceFunctions
    {
        private static TimingCard timingcard = new TimingCard();
        private static NetworkCard networkcard = new NetworkCard();
        private static InfaredCard infaredcard = new InfaredCard();
        private static UsbCard usbcard = new UsbCard();
        private static Random randomSelect = new Random();

        public static bool Init()
        {
            ClockProbe.Init();
            DeviceProbe.Init();
            DeviceProbe.SendExternal(new TrafficMessages()
            {
                External = "All Device Functions initialized"
            });
            timingcard.Init();
            networkcard.Init();
            infaredcard.Init();
            usbcard.Init();            
            return true;
        }

        public static bool Close()
        {
            DeviceProbe.SendExternal(new TrafficMessages()
            {
                External = "All Device Functions shutting down"
            });

            networkcard.Close();
            infaredcard.Close();
            usbcard.Close();
            timingcard.Close();
            DeviceProbe.Close();
            ClockProbe.Close();
            return true;
        }

        public static bool Send()
        {
            DeviceProbe.SendExternal(new TrafficMessages()
            {
                External = string.Format("Executing: {0}\nExecuting: {1}\nExecuting: {2}", 
                nameof(NetworkCard), 
                nameof(InfaredCard), 
                nameof(UsbCard))
            });

            //timingcard.ExecuteTask();
            networkcard.ExecuteTask(randomSelect.Next(5));
            infaredcard.ExecuteTask(randomSelect.Next(5));
            usbcard.ExecuteTask(randomSelect.Next(5));

            return true;
        }
    }
}
