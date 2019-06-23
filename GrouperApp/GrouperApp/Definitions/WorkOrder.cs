using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrouperApp.Definitions
{
    public class WorkOrder
    {
        public class WorkOrderArg : EventArgs
        {
            public string ID { get; set; }
            public int GroupID { get; set; }
            public int Shifts { get; set; }
            public uint Progress { get; set; }
            public string Timestamp { get; set; }
        }

        /// <summary>
        /// Progress Change Handler to handle updates from external process
        /// </summary>
        public EventHandler<WorkOrderArg> ProgessChanged;

        public uint Progress { get; set; }
        public string Start { get; set; }
        public string End { get; set; }

        public WorkOrder ()
        {
            //Automatic Register Progress Listener
            this.ProgessChanged += new EventHandler<WorkOrderArg>((sender,evt) =>
            {
                if(Progress == 0)
                {
                    Start = DateTime.Now.ToShortDateString();
                }
                Progress = evt.Progress;
                if (Progress >= 100)
                {
                    End = DateTime.Now.ToShortDateString();
                }
            });
        }
    }
}
