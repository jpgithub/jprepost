using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WpfApplication2.Backend;
using WpfApplication2.Model;

namespace WpfApplication2.ViewModel
{
    internal class ImgViewModel
    {
        private ImageAnalyzerModel IAModel;
        private DialogViewModel dlgviewmodel;

        private SubscribeAsByteStream listener;
        private byte[] byteStream;
       

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


    }
}
