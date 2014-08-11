using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using AppServer.Model;
using AppServer.Backend;
using AppServer.Misc;

namespace AppServer.ViewModel
{
    internal class ServerViewModel
    {
        private DataLogger logStreamOutput;
        private PortModel portModel;
        public delegate void RequestOpenFileDialogBox();
        private RequestOpenFileDialogBox openfiledialogbox;
        private string filename;
        private bool isBadFilename;
        private const string BadFileNameErrorMsg = "Bad Filename or No Filename, Can't Open File";
        private NetworkAccess appserver;
        public ServerViewModel()
        {
            isBadFilename = true;
            logStreamOutput = new DataLogger();
            OpenFileCmd = new OpenFile(this.OpenFileAction);
            StartCmd = new StartVirtualHardware(this.StartVirtualHardwareAction);
            StopCmd = new StopVirtualHardware(this.StopVirtualHardwareAction);
            portModel = new PortModel();
            portModel.IsEditable = true;
        }

        public ServerViewModel(RequestOpenFileDialogBox callback) : this()
        {
            openfiledialogbox = callback;
        }

        public DataLogger LogStreamOutput
        {
            get
            {
                return logStreamOutput;
            }
        }

        public PortModel PortModel
        {
            get
            {
                return portModel;
            }
        }

        public ICommand OpenFileCmd
        {
            get;
            private set;
        }

        public ICommand StartCmd
        {
            get;
            private set;
        }

        public ICommand StopCmd
        {
            get;
            private set;
        }

        private class StopVirtualHardware : ICommand
        {
            public delegate void ActionMethod();
            private ActionMethod stopVirtualHardwareAction;

            public StopVirtualHardware(ActionMethod method)
            {
                stopVirtualHardwareAction = method;

            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public void Execute(object parameter)
            {
                stopVirtualHardwareAction();
            }
        }

        public void StopVirtualHardwareAction()
        {
            if (appserver != null)
            {
                appserver.StopServer();
            }
            else
            {
                logStreamOutput.DebugLog += "\nServer is already offline";
            }
        }

        private class StartVirtualHardware : ICommand
        {
            public delegate void ActionMethod();
            private ActionMethod startVirtualHardwareAction;
            
            public StartVirtualHardware(ActionMethod method)
            {
                startVirtualHardwareAction = method;
                
            }
            
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public void Execute(object parameter)
            {
                startVirtualHardwareAction();
            }
        }

        public void StartVirtualHardwareAction()
        {
            if (!isBadFilename)
            {
                FileStreamReader myreader = new FileStreamReader(filename);
                logStreamOutput.DebugLog += "Port Number is " + portModel.PortNumber;
                portModel.IsEditable = false;
                appserver = new NetworkAccess(myreader.Stream,portModel.PortNumber);
                appserver.NetworkStatus += listenNetworkStatus;
                appserver.ServerStatus += listenServerStatus;
                Thread spawn = new Thread(new ThreadStart(appserver.StartServer));
                spawn.Start();
               
            }
            else
            {
                logStreamOutput.DebugLog = BadFileNameErrorMsg;
            }
        }

        private class OpenFile : ICommand
        {
            public delegate void ActionMethod();
            private ActionMethod openfileAction;

            public OpenFile(ActionMethod method)
            {
                openfileAction = method;
            }
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public void Execute(object parameter)
            {
                openfileAction();
            }
        }

        public void OpenFileAction()
        {
            openfiledialogbox();
            //throw new NotImplementedException();
        }

        public string SetFileName
        {
            set
            {
                if (value != string.Empty)
                {
                    this.filename = value;
                    isBadFilename = false;
                    
                }
            }
        }

        private void listenNetworkStatus(object sender, ErrorEventArgs e)
        {
            logStreamOutput.DebugLog = e.GetException().ToString();
        }

        private void listenServerStatus(object sender, MessageEventArgs e)
        {
            logStreamOutput.ConnectionLog = e.Message;
        }
    }
}
