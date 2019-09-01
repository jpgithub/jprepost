using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WebHubDefinitonLib;

namespace WebHubAppSR.Hubs
{
    public class RouterEngineHub : Hub<IRouterIPC>
    {
        public async Task AddToGroup(string grpName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, grpName);
        }

        public async Task SendRouterEngineStatusToClients(string grpname, RouterMessage msg)
        {
            await Clients.Group(grpname).SendRouterIPC(msg);
        }

        public async Task RemoveFromGroup(string grpName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, grpName);
        }
    }
}
