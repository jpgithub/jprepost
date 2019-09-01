using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHubDefinitonFWLib
{
    public interface ITrafficMessage
    {
        Task ShowTrafficMsg(TrafficMessages message);
    }
}
