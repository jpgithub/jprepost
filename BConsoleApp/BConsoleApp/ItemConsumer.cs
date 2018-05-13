using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BConsoleApp
{
    public class ItemConsumer : Consumer<DataSubFrame>
    {
        private int state = 0;

        protected override void Process()
        {
            //Try taking from BlockingCollection
            while (this.Buffer.TryTake(out AsynchronousBufferItem item, System.Threading.Timeout.Infinite))
            {
                // Iterate through Words.Lenth
                foreach(var word in item.Words)
                {
                    ExecuteStateMachine(item);
                }

                if (this.Buffer.IsCompleted)
                    break;
            }
        }

        protected override void ExecuteStateMachine(AsynchronousBufferItem item)
        {
            var subFrame = new DataSubFrame()
            {
                Header = item.ID,
                Body = item.Words[0].ToString(),
                Footer = item.Words[1].ToString()
            };
            switch (state)
            {
                case 0:
                    this.state = 1;
                    break;
                case 1:
                    this.state = 2;
                    break;
                case 2:
                    this.state = 3;
                    break;
                case 3:
                    this.Elements.Add(subFrame);
                    this.state = 0;
                    //this.ItemReceived.Invoke(this,subFrame);
                    break;
            }
        }

    }
}
