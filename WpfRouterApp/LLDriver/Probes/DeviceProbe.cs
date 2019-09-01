using Microsoft.AspNetCore.SignalR.Client;
using System.Threading;
using WebHubDefinitonFWLib;

namespace LLDriver.Probes
{
    public class DeviceProbe
    {
        private readonly static HubConnection connection = new HubConnectionBuilder().WithUrl(UrlStrings.HubUrls.DeviceProbeHub).Build();
        private static CancellationTokenSource source = new CancellationTokenSource();

        public static async void Init()
        {
            try
            {
                
                await connection.StartAsync();
            }
            catch
            {
                ;
            }
        }

        internal static async void SendInternal(TrafficMessages msgs)
        {
            if (connection.State == HubConnectionState.Disconnected)
                return;

            await connection.SendAsync("SendTrafficMsgToClients", msgs, source.Token);
        }

        public static async void SendExternal(TrafficMessages msgs)
        {
            if (connection.State == HubConnectionState.Disconnected)
                return;

            await connection.SendAsync("SendTrafficMsgToClients", msgs, source.Token);
        }

        public static async void Close()
        {
            source.Cancel();
            await connection.StopAsync();
        }
    }
}
