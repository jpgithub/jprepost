using AppServer.Misc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Backend
{
    public interface INetworkStatus
    {
        event ErrorEventHandler NetworkStatus;
        event EventHandler<MessageEventArgs> ServerStatus;
    }
}
