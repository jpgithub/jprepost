using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarbageConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //63 bytes in file
            int counter = 0;
            //var bytes = File.ReadAllBytes(args[0]);
            //var bytessize = bytes.Length;

            var prod = new Producer(args[0]);

            prod.TestCase();
            
            //foreach (BytesFrame frame in prod.ReadFrames())
            //{ 
            //    string ans = string.Format("Counter: {0}, Value: {1}",frame.GetID,frame.GetCollection.Count.ToString());
            //    if (prod.GetID-frame.GetID != 1)
            //    {
            //        Console.Out.WriteLineAsync(string.Format("Skipped Counter: {0}, Value: {1}", frame.GetID, frame.GetCollection.Count.ToString()));
            //        break;
            //    }
            //}

            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
        }

    }

    public class Producer
    {
        private int id = 0;
        private string filename;
        private List<int> collection;
        public Producer(string name)
        {
            this.filename = name;
            this.collection = new List<int>();
        }

        public IEnumerable<BytesFrame> ReadFrames()
        {
            using (FileStream reader = File.Open(filename,FileMode.Open,FileAccess.Read))
            {
                byte [] datarray = new byte[4410];
                
                while (reader.CanRead)
                {
                    reader.Read(datarray,0, datarray.Length);
                    yield return new BytesFrame(id++,datarray);
                }
            }
        }

        public int GetID
        {
            get
            {
                return this.id;
            }
        }

        public void TestCase()
        {
            var frames = ReadFrames();
            DoubleSize(frames);
            Console.WriteLine(frames.First().mySize);
            /*
             * 
             * 
             
    When the line var frames = ReadFrames(); is executed we’re not getting a list of frames, we’re getting a state-machine that can create frames.
    That state machine is then passed to the DoubleSize-method.
    Inside the DoubleSize-method we use the state-machine to generate the frames and we double the amount of each of those frames.
    All the frames that were created are discarded though, as there are no references to them.
    When we return to the main method, we still have a reference to the state-machine. By calling the First-method we again ask it to generate frames (only one in this case). 
    The state-machine again creates an frame. This is a new frame and as a result, the size will be 4410.
 
             * 
             * 
             * 
             */
        }

        void DoubleSize(IEnumerable<BytesFrame> frames)
        {
            int i = 0;
            foreach (var frame in frames)
            {
                frame.mySize = frame.mySize * 2;
                if (i > 2)
                {
                    break;
                }
                i++;
            }
        }
    }

    public class BytesFrame
    {
        private int size = 0;
        private int id = 0;
        private List<int> valuelist = new List<int>();

        public BytesFrame(int count,byte[] rawbytes)
        {
            this.id = count;
            size = rawbytes.Length;
            for (int i = 0; i < rawbytes.Length; i += 2)
            {
                valuelist.Add(BitConverter.ToUInt16(rawbytes, i));
            }

            //throw a fault error bubbled
            //if (id == 10000)
            //{
            //    throw new Exception("faulted");
            //}
        }

        public int mySize
        {
            get
            {
                return this.size;
            }
            internal set
            {
                this.size = value;
            }
        }

        public ReadOnlyCollection<int> GetCollection
        {
            get
            {
                return new ReadOnlyCollection<int>(valuelist);
            }
        }

        public int GetID
        {
            get
            {
                return this.id;
            }
        }
    }
}
