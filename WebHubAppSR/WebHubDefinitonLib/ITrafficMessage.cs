using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebHubDefinitonLib
{
    public interface ITrafficMessage
    {
        Task ShowTrafficMsg(TrafficMessages message);
    }
}
