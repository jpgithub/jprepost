using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BConsoleApp
{
    public class DataSubFrame : EventArgs
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public string Footer { get; set; }
    }
}
