using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using WebHubDefinitonLib;

namespace WebHubAppSR.Hubs
{
    public class ClockHub : Hub<IClock>
    {
        public async Task SendTimeToClients(DateTime dateTime)
        {
            await Clients.All.ShowTime(dateTime);
        }
    }
}
