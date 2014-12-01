using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Hardware
{
    public abstract class Instrument
    {
        /// <summary>
        /// Instrument Argument
        /// </summary>
        public class InstrumentArgs : EventArgs
        {
            public byte[] bytestream { get; set; }
            public int registerValue { get; set; }
            public string registerValue2 { get; set; }
        }

        /// <summary>
        /// Client connected to computer will be notify of via this handler
        /// </summary>
        public delegate void InstrumentEventHandler(object sender, InstrumentArgs e);

        abstract public void AddListener(InstrumentEventHandler clientHandler);
        abstract public void DetachListener(InstrumentEventHandler clientHandler);
    }

    internal class Computer : Instrument
    {
        // Driver for computer
        internal class Driver
        {
            private int total = int.MinValue;
            private int threshold = 10000;
            public event EventHandler<ThresholdReachedEventArgs> ThresholdReached;

            protected virtual void OnThresholdeReached(ThresholdReachedEventArgs e)
            {
                EventHandler<ThresholdReachedEventArgs> handler = ThresholdReached;
                if (handler != null)
                {
                    handler(this, e);
                }
            }

            internal class ThresholdReachedEventArgs : EventArgs
            {
                public int Threshold { get; set; }
                public DateTime TimeReached { get; set; }
            }

            public void DriverStart()
            {
                new Thread(() =>
                {
                    total += 1;

                    if (total >= threshold)
                    {
                        ThresholdReachedEventArgs args = new ThresholdReachedEventArgs();
                        args.Threshold = threshold;
                        OnThresholdeReached(args);
                    }

                    Thread.Sleep(20);

                }).Start();
            }

        }

        private Driver driver;

        /// <summary>
        /// An Instrument Event Handler
        /// </summary>
        private event InstrumentEventHandler InstrumentChanged;

        public Computer()
        {
            driver = new Driver();

            // Computer load the driver by registering it with a handler
            driver.ThresholdReached += driver_ThresholdReached;
        }

        /// <summary>
        /// This driver tell computer its status
        /// </summary>
        /// <param name="sender"> driver </param>
        /// <param name="e"> driver threshold </param>
        private void driver_ThresholdReached(object sender, Driver.ThresholdReachedEventArgs e)
        {
            // Threshold Reached notify computer 
            InstrumentArgs args = new InstrumentArgs();
            args.registerValue = e.Threshold;
            computer_OnThresholdReached(args);
        }

        // Client will connect with Computer 
        public override void AddListener(InstrumentEventHandler clientHandler)
        {
            InstrumentChanged += clientHandler;
        }

        // Cient will disconnect with Computer
        public override void DetachListener(InstrumentEventHandler clientHandler)
        {
            InstrumentChanged -= clientHandler;
        }

        /// <summary>
        /// Client connected to computer will be notify of via this handler
        /// </summary>
        /// <param name="e"> Instrument Argument </param>
        protected virtual void computer_OnThresholdReached(InstrumentArgs e)
        {
            //InstrumentChanged = computerHandler;
            if (InstrumentChanged != null)
            {
                InstrumentChanged(this, e);
            }
        }
    }
}
