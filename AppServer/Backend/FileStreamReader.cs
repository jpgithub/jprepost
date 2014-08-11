using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppServer.Backend
{
    internal class FileStreamReader 
    {
        private byte[] srcByteStream;
        
        public FileStreamReader(string filepath, long maxfilesize = 1024000)
        {
            using (FileStream fs = File.OpenRead(filepath))
            {
                if ((fs.Length/1024) < maxfilesize)
                    srcByteStream = new BinaryReader(fs).ReadBytes((int)fs.Length);
            }
            
        }

        public byte[] Stream
        {
            get
            {
                return srcByteStream;
            }
        }

        
    }
}
