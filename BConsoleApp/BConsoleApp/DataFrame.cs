using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BConsoleApp
{
    public class DataFrame
    {
        private List<ushort> words = new List<ushort>();
        public DataFrame(byte[] payload)
        {
            int delimter = 16;
            int counter = 0;
            int limit = 0;
            while (limit < payload.Length)
            {
                this.words.Add(BitConverter.ToUInt16(payload, counter));

                limit = (counter++ * delimter);
            }

            Words = this.words.ToArray();
        }
        public ushort[] Words { get; private set; }
    }
}
