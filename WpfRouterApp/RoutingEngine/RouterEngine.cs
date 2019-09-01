using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoutingEngine
{
    public class RouterEngine
    {
        private const int limit = 100;
        private CancellationTokenSource cancelsrc = new CancellationTokenSource();

        public Task StartEngine(string scenario)
        {
            return Task.Factory.StartNew(() =>
            {
                int i = 0;
                HLDriver.DeviceFunctions.Init();
                RouterAppInterface.Init();
                try
                {
                    while (i < limit)
                    {
                        HLDriver.DeviceFunctions.Send();
                        uint progress = 0;

                        while (progress < 101)
                        {
                            cancelsrc.Token.ThrowIfCancellationRequested();
                            RouterAppInterface.SendIPCUpdate(new WebHubDefinitonFWLib.RouterMessage()
                            {
                                TaskID = i.ToString(),
                                Progress = progress
                            });
                            progress += 10;
                            Task.Delay(100).Wait();
                        }

                        Task.Delay(500).Wait();
                        i++;
                    }
                }
                catch (OperationCanceledException)
                {
                    ;
                }

                RouterAppInterface.Close();
                HLDriver.DeviceFunctions.Close();

            }, cancelsrc.Token);
        }

        public void StopEngine()
        {
            cancelsrc.Cancel();
        }
    }
}
