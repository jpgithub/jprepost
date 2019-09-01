using System;
using System.Collections.Generic;
using System.Text;

namespace WebHubDefinitonLib
{
    public class UrlStrings
    {
        public static class Events
        {
            public static string TimeSent => nameof(IClock.ShowTime);
            public static string RouterIPCSend => nameof(IRouterIPC.SendRouterIPC);
            public static string TrafficMsgSend => nameof(ITrafficMessage.ShowTrafficMsg);
        }

        public static string ReIPCGroupName = "RTIPC";

        public static class HubUrls
        {
#if LOCAL
            public static string DeviceProbeHub => "http://localhost:54449/traffichub";
            public static string RTClockHub => "http://localhost:54449/clockHub";
            public static string RouterEngineHub => "http://localhost:54449/routerenginehub";
#else
            public static string DeviceProbeHub => "http://localhost:54449";
            public static string RTClockHub => "http://localhost:54449";
            public static string RouterEngineHub => "http://localhost:54449/routerenginehub";
#endif
        }
    }
}
