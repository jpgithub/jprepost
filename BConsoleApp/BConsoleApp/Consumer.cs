using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BConsoleApp
{
    public abstract class Consumer<T>
    {
        //public event EventHandler<T> ItemReceived;

        public Consumer()
        {
            this.Elements = new List<T>();
            this.Buffer = new BlockingCollection<AsynchronousBufferItem>();
        }

        public Task ProcessAsync()
        {
            return Task.Factory.StartNew(Process);
        }

        protected abstract void Process();
        
        public void EndProcessing()
        {
            this.Buffer.CompleteAdding();
        }

        protected abstract void ExecuteStateMachine(AsynchronousBufferItem item);

        public List<T> Elements { get; private set; }
        public BlockingCollection<AsynchronousBufferItem> Buffer { get; private set; }
    }
}
