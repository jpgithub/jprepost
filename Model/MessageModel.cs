using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2.Model
{
    public class MessageModel : BaseModel
    {
        private string labelcontent;
        private string callbacklog;

        public string LabelContent
        {
            get
            {
                return labelcontent;
            }
            set
            {
                this.labelcontent = value;
                OnPropertyChanged("LabelContent");
            }
        }

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
    }
}
