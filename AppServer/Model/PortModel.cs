using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Model
{
    public class PortModel : BaseModel
    {
        private int portnumber;
        private bool isEditablePortField;

        public int PortNumber
        {
            get
            {
                return portnumber;
            }
            set
            {
                this.portnumber = value;
                OnPropertyChanged("PortNumber");
            }
        }

        public bool IsEditable
        {
            get
            {
                return isEditablePortField;
            }
            set
            {
                this.isEditablePortField = value;
                OnPropertyChanged("IsEditable");
            }
        }


    }
}
