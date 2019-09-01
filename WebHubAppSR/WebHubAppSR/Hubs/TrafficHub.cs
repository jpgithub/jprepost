using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebHubDefinitonLib;

namespace WebHubAppSR.Hubs
{
    public class TrafficHub : Hub<ITrafficMessage>
    {
        public async Task SendTrafficMsgToClients(TrafficMessages msgs)
        {
            await Clients.All.ShowTrafficMsg(msgs);
        }
    }
}
