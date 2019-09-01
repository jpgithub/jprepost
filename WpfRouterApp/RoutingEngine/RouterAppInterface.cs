using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebHubDefinitonFWLib;

namespace RoutingEngine
{
    internal class RouterAppInterface
    {
        private readonly static HubConnection connection = new HubConnectionBuilder().WithUrl(UrlStrings.HubUrls.RouterEngineHub).Build();
        private static CancellationTokenSource source = new CancellationTokenSource();

        internal static async void Init()
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

        internal static async void SendIPCUpdate(RouterMessage ipc)
        {
            if (connection.State == HubConnectionState.Disconnected)
                return;

            await connection.SendAsync("SendRouterEngineStatusToClients", UrlStrings.ReIPCGroupName ,ipc, source.Token);
        }

        internal static async void Close()
        {
            source.Cancel();
            await connection.StopAsync();
        }
    }
}
