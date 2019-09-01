using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;
using WebHubDefinitonFWLib;

namespace LLDriver.Probes
{
    public class ClockProbe
    {
        private readonly static HubConnection connection = new HubConnectionBuilder().WithUrl(UrlStrings.HubUrls.RTClockHub).Build();
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

        internal static async void SendTime(DateTime rt)
        {
            if (connection.State == HubConnectionState.Disconnected)
                return;

            await connection.SendAsync("SendTimeToClients", rt, source.Token);
        }

        public static async void Close()
        {
            source.Cancel();
            await connection.StopAsync();
        }
    }
}
