using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication2.Model;

namespace WpfApplication2.Backend
{
    class ImageSubscriber //Listener
    {
        protected RandomizeImage imgRandomizer;
        protected ImageAnalyzerModel iam;
        //Listener will pass this function to the event handler allow it to call this function ever time the event is triggered.
        protected virtual void OnCallQueryImage(object sender, ImageEventHandler.ImageEventHandlerEventArgs e){}

        public void Add()
        {
            imgRandomizer.Spawn += OnCallQueryImage;
        }
        public void Detach()
        {
            imgRandomizer.Spawn -= OnCallQueryImage;
            imgRandomizer = null;
        }
        
        
    }
}
