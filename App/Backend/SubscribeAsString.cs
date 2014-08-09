using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication2.Model;

namespace WpfApplication2.Backend
{
    class SubscribeAsString: ImageSubscriber
    {
        public SubscribeAsString(RandomizeImage Ri, ImageAnalyzerModel Iam)
        {
            imgRandomizer = Ri;
            iam = Iam;
        }
        protected override void OnCallQueryImage(object sender, ImageEventHandler.ImageEventHandlerEventArgs e)
        {
            iam.ImageFileName = e.ImageName;
        }

    }
}
