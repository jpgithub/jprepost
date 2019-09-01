using System;
using System.Threading.Tasks;

namespace WebHubDefinitonLib
{
    public interface IClock
    {
        Task ShowTime(DateTime currentTime);
    }
}
