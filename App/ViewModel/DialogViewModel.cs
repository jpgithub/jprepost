using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WpfApplication2.Backend;
using WpfApplication2.Model;

namespace WpfApplication2.ViewModel
{
    internal class DialogViewModel
    {
        private MessageModel LC;

        private Thread imgthread;
        private RandomizeImage Rimg;
        

        public DialogViewModel()
        {
            LC = new MessageModel();
            LC.LabelContent = "Virtual NetAcquirer";

            #region Simulate NetAcquireCallback
            ///Register Commands
  
            Rimg = new RandomizeImage();
            StartCmd = new StartThread(this.StartThreadAction);
            StopCmd = new StopThread(this.StopThreadAction);

            #endregion

        }

        public MessageModel MessageModel
        {
            get
            {
                return LC;
            }
        }

        public RandomizeImage ImgSrcObject
        {

            get
            {
                return Rimg;
            }
            
        }

        private class StartThread : ICommand
        {
            public delegate void ActionMethod();
            private ActionMethod StartThreadAction;

            public StartThread(ActionMethod method)
            {
                StartThreadAction = method;
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
                StartThreadAction();
            }
        }

        public void StartThreadAction()
        {
            try
            {
                imgthread = new Thread(Rimg.SpawningByteStream);
                imgthread.Start();

            }
            catch (ThreadStartException)
            {
                throw;
            }
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

        public void StopThreadAction()
        {
            try
            {
                imgthread.Abort();
                //imgthread.Join();
            }
            catch (ThreadStateException)
            {
                throw;

            }
            catch (ThreadAbortException)
            {
                Console.WriteLine("Attempt to Abort Thread Fail");
            }
            catch (System.Security.SecurityException)
            {
                throw;
            }
        }

        private class StopThread : ICommand
        {
            public delegate void ActionMethod();
            private ActionMethod StopThreadAction;

            public StopThread(ActionMethod method)
            {
                StopThreadAction = method;
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
                StopThreadAction();
            }
        }

        
    }
}
