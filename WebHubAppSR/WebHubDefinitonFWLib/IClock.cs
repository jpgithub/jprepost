using System;
using System.Threading.Tasks;

namespace WebHubDefinitonFWLib
{
    public interface IClock
    {
        Task ShowTime(DateTime currentTime);
    }
}
