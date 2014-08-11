using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Model
{
    internal class DataLogger : BaseModel
    {
        private string callbacklog;
        private string connectionlog;

        public string DebugLog
        {
            get
            {
                return callbacklog;
            }
            set
            {
                this.callbacklog = value;
                OnPropertyChanged("DebugLog");
            }
        }

        public string ConnectionLog
        {
            get
            {
                return connectionlog;
            }
            set
            {
                this.connectionlog = value;
                OnPropertyChanged("ConnectionLog");
            }
            
        }


    }
}
