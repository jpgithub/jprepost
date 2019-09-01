using System.Threading.Tasks;

namespace WebHubDefinitonFWLib
{
    public interface IRouterIPC
    {
        Task SendRouterIPC(RouterMessage message);
    }
}
