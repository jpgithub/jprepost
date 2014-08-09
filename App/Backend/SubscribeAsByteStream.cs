using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WpfApplication2.Model;
using WpfApplication2.ViewModel;

namespace WpfApplication2.Backend
{
    class SubscribeAsByteStream:ImageSubscriber
    {
        private ImgViewModel ivm;
        //public SubscribeAsByteStream(RandomizeImage Ri, ImageAnalyzerModel Iam) : base(Ri, Iam) { }
        private StringBuilder debugmsg;
        public SubscribeAsByteStream(RandomizeImage Ri, ImgViewModel Ivm)
        {
            base.imgRandomizer=Ri;
            this.ivm = Ivm;
            debugmsg = new StringBuilder();
            
        }

        protected override void OnCallQueryImage(object sender, ImageEventHandler.ImageEventHandlerEventArgs e)
        {
            ivm.ByteStream =  e.ImageByteStream;
            debugmsg.AppendLine("OnCallQueryImage Got Byte Stream of Size " + e.ImageByteStream.Length.ToString() + " Bytes");
            ivm.DlgViewModel.MessageModel.DebugLog = debugmsg.ToString();
        }

    }
}
