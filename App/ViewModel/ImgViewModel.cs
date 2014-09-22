using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WpfApplication2.Backend;
using WpfApplication2.Model;
using WpfApplication2.Properties;

namespace WpfApplication2.ViewModel
{
    internal class ImgViewModel  
    {

        readonly Dispatcher _dispatcher;
        private ImageAnalyzerModel IAModel;
        private DialogViewModel dlgviewmodel;

        private SubscribeAsByteStream listener;
        private byte[] byteStream;

        private DispatcherTimer frametimer = new DispatcherTimer();
        //private double frameInterval = 32;

        private LinearGradientBrush mybrush;

        private DisplaySource console;
            
        public ImgViewModel()
        {
            IAModel = new ImageAnalyzerModel();
            //IAModel.ImageFileName = "../Resources/Ha3.jpg";
            //IAModel.NetImage = new BitmapImage(new Uri("C:/Users/JP/documents/visual studio 2012/Projects/WpfApplication2/WpfApplication2/Resources/Ha3.jpg"));
            dlgviewmodel = new DialogViewModel();

            #region Attached a Listener aka callback function
            ///Register Commands
            QueryImageCmd = new QueryImage(this.QueryImageAction);
            StopQueryCmd = new StopQuery(this.StopQueryAction);

            
            //listener = new SubscribeAsString(Rimg, IAModel);
            listener = new SubscribeAsByteStream(DlgViewModel.ImgSrcObject, this);

            #endregion

            GPIO = new GenericTestCmd(this.GenericTestAction);

            string value = WpfApplication2.Properties.Resources.GreyBlackHotBrush.ToString();
            
            mybrush = Application.Current.FindResource(value) as LinearGradientBrush;
            try
            {
                //hard coded
                console = new DisplaySource(mybrush.GradientStops.Cast<object>().ToList(), this.DisplaySourceOutput);
                 
            }
            catch (ResourceReferenceKeyNotFoundException e)
            {
                Console.Out.WriteLineAsync("Resource Exception: " + e.Message);
            }

            /// Assuming this viewmodel is not being created by a background thread but a UI thread then give it a name            
            Dispatcher.CurrentDispatcher.Thread.Name = "My App UI Thread";
            _dispatcher = Dispatcher.CurrentDispatcher;

        }
        
        public string ImageFileName
        {
            get
            {
                return IAModel.ImageFileName;
            }

            set
            {
                this.IAModel.ImageFileName=value;
            }
        }

        public string ImageLocation
        {
            get
            {
                return IAModel.MouseLocation;
            }

            set
            {
                this.IAModel.MouseLocation = value;
            }
        }

        public ImageAnalyzerModel ImageAnalyzerModel
        {
            get
            {
                return IAModel;
            }
        }

        public DialogViewModel DlgViewModel
        {
            get
            {
                return dlgviewmodel;
            }    
        }

        public void DisplaySourceOutput(object frame)
        {
            if (frame != null)
            {
                string color = (frame as GradientStop).Color.ToString();
                Console.Out.WriteLineAsync("Output: " + color);

            }

            //console.Rewind();
            //console.Play();

        }

        public ICommand QueryImageCmd
        {
            get;
            private set;
        }

        public ICommand StopQueryCmd
        {
            get;
            private set;
        }

        public ICommand GPIO
        {
            get;
            private set;
        }

        private class GenericTestCmd : ICommand
        {
            public delegate void ActionMethod();
            private ActionMethod GenericTestAction;

            public GenericTestCmd(ActionMethod method)
            {
                GenericTestAction = method;
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
                GenericTestAction();
            }
            
        }

        private void GenericTestAction()
        {
            if (!bw.IsBusy)
            {
                InitializedBackgroundWorker();
                bw.RunWorkerAsync();
            }
            //console.Play();
            //Check the setted ui thread name
            //Console.Out.WriteLineAsync("My UI Thread Name: "+Dispatcher.CurrentDispatcher.Thread.Name);
         

        }

        public void QueryImageAction()
        {
            listener.Add();
        }

        public void StopQueryAction()
        {
            listener.Detach();
        }

        private class QueryImage : ICommand
        {
            public delegate void ActionMethod();
            private ActionMethod QueryImageAction;

            public QueryImage(ActionMethod method)
            {
                QueryImageAction = method;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged
            {
                add{ CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public void Execute(object parameter)
            {
                QueryImageAction();
            }
        }

        private class StopQuery : ICommand
        {
            public delegate void ActionMethod();
            private ActionMethod StopQueryAction;

            public StopQuery(ActionMethod method)
            {
                StopQueryAction = method;
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
                StopQueryAction();
            }
        }

        public byte[] ByteStream
        {
            set
            {
                this.byteStream = value;
                UpdateImage();
            }
        }

        private void UpdateImage()
        {
            //Console.WriteLine("Img View Model Got Byte Stream's Length {0} ",byteStream.Length);
            BitmapImage RecvImg = new BitmapImage();
            RecvImg.BeginInit();
            RecvImg.StreamSource = new MemoryStream(byteStream);
            RecvImg.EndInit();

            if ((RecvImg != null) && (RecvImg.CanFreeze))
            {
                RecvImg.Freeze();
                IAModel.NetImage = RecvImg;
            }
            //else
            //{
            //    Console.Error.WriteLine("RecImg return Null");
            //    IAModel.NetImage = new BitmapImage(new Uri("C:/Users/JP/documents/visual studio 2012/Projects/WpfApplication2/WpfApplication2/Resources/Ha3.jpg"));
            //}
        }

        #region Background Worker
        //http://stackoverflow.com/questions/9159203/set-up-backgroundworker-mvvm-update-a-progressbar
        //http://stackoverflow.com/questions/5483565/how-to-use-wpf-background-worker

        /// <summary>
        /// Background Worker instance
        /// </summary>
        private readonly BackgroundWorker bw = new BackgroundWorker();

        /// <summary>
        /// Initialize Background
        /// </summary>
        public void InitializedBackgroundWorker()
        {
            bw.WorkerReportsProgress = true;
            //bw.WorkerSupportsCancellation = true;

            bw.DoWork += new DoWorkEventHandler(bwDoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwRunWorker);
            bw.ProgressChanged += new ProgressChangedEventHandler(bwProgress);
        }

        private void bwDoWork( object sender , DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            //Do Work
            for (int i = 1; (i <= 10); i++)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(500);
                    worker.ReportProgress((i * 10));
                }

            }


            //worker.ReportProgress((int)e.Argument);
            //e.Result = ImageAnalyzerModel.ProgressValue;
        }

        private void bwRunWorker(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                ImageAnalyzerModel.ProgressValue = 0;
            }
            else if (!(e.Error == null))
            {
                ;
            }
            else
            {
                Console.Out.WriteLine("Done");
            }
        }

        private void bwProgress(object sender, ProgressChangedEventArgs e)
        {
            ImageAnalyzerModel.ProgressValue = e.ProgressPercentage;
        }

        #endregion


    }
}
