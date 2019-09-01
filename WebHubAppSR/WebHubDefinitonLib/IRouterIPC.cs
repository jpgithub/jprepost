using System.Threading.Tasks;

namespace WebHubDefinitonLib
{
    public interface IRouterIPC
    {
        Task SendRouterIPC(RouterMessage message);
    }
}
