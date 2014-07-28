using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2.Backend
{
    class ImageEventHandler
    {
        public event EventHandler<ImageEventHandlerEventArgs> Spawn;

        protected virtual void OnSpawnChanged(ImageEventHandlerEventArgs e)
        {
            if (Spawn != null)
                Spawn(this, e);

        }

        public class ImageEventHandlerEventArgs : EventArgs
        {
            public string ImageName
            {
                get;
                set;
            }
            public byte[] ImageByteStream
            {
                get;
                set;
            }
            public Stream ImageStream
            {
                get;
                set;
            }
        }


    }
}
