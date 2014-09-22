using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WpfApplication2.Backend
{
    public static class PlaybackSpeedConstants
    {
        public readonly static uint NormalSpeed = uint.Parse(WpfApplication2.Properties.Resources.Normal.ToString());

        public readonly static uint FastSpeed = uint.Parse(WpfApplication2.Properties.Resources.Fast.ToString());

        public readonly static uint FasterSpeed = uint.Parse(WpfApplication2.Properties.Resources.Faster.ToString());

        public readonly static uint SlowSpeed = uint.Parse(WpfApplication2.Properties.Resources.Slow.ToString());

        public readonly static uint SlowerSpeed = uint.Parse(WpfApplication2.Properties.Resources.Slower.ToString());
        
    }

    internal class DisplaySource : DispatcherTimer , IDisplay
    {
        public delegate void DisplaySourceOutput(object frame);
        private uint frameInterval;
        private List<object> frames;
        private IEnumerator frameIterator;
        private ReverseEnumerator frameRiterator;
        private bool isPause;
        private DisplaySourceOutput OutputSource;// DisplaySource Will Output it stuff to OutputSource
        private object currentFrame;
        private bool isReverse;
        private object beginFrame;
        private int currentIndex;

        public DisplaySource(IList<object> SourceData, DisplaySourceOutput Output)
        {
            frameInterval = PlaybackSpeedConstants.NormalSpeed; //30 Frame per sec
            Interval = TimeSpan.FromMilliseconds(frameInterval);
            Tick += SendToOutput;
            frames = SourceData as List<object>;
            OutputSource = Output;
            frameIterator = frames.GetEnumerator();
            frameRiterator = new ReverseEnumerator();
            beginFrame = frames.First();
           
        }

        public DisplaySource(DispatcherPriority dp, Dispatcher d, IList<object> SourceData, DisplaySourceOutput Output)
            : base(dp,d) 
        {
            frameInterval = PlaybackSpeedConstants.NormalSpeed; //30 Frame per sec
            Interval = TimeSpan.FromMilliseconds(frameInterval);
            Tick += SendToOutput;
            frames = SourceData as List<object>;
            OutputSource = Output;
            frameIterator = frames.GetEnumerator();
            frameRiterator = new ReverseEnumerator();
            beginFrame = frames.First();
        }

        /// <summary>
        /// Reset Dispatching of frames to the beginning
        /// </summary>
        private void ResetDispatcher()
        {
            Stop();
            frameIterator.Reset();
            isReverse = false;
            ///frames.Sort();
        }

        private void StartDispatcher()
        {
            if (!IsEnabled)
                Start();
        }

       
        /// <summary>
        /// Playback in reverse
        /// </summary>
        /// <returns></returns>
        private bool ReversePlayback()
        {
            if (isReverse)
            {
                return frameRiterator.MoveNext();
            }
            else
            {
                return isReverse;
            }
        }

        private bool PlayBack()
        {
            // Not in reverse mode
            if (!isReverse)
            {
                return frameIterator.MoveNext();
            }
            else
            {
                //In reverse mode turn off regular playback
                return !isReverse;
            }

        }

        private void SendToOutput(object sender, EventArgs e)
        {
            
            if (ReversePlayback())
            {
                currentFrame = frames[(int)frameRiterator.Current];
                currentIndex = frames.IndexOf(currentFrame);
            
            }else if (PlayBack())
            {
                currentFrame = frameIterator.Current;
                currentIndex = frames.IndexOf(currentFrame);
            }else // Reach the end
            {
                ResetDispatcher();
            }

            OutputSource(currentFrame);
        }

        private void SetDispatchSpeed(uint TimeInterval)
        {
            if (frameInterval != TimeInterval)
            {
                frameInterval = TimeInterval;
                Interval = TimeSpan.FromMilliseconds(frameInterval);
            }
            
        }

        private void ReverseIterator()
        {
            frameRiterator.Begin = frames.IndexOf(currentFrame);
            if( (int)frameRiterator.Current > 0)
                isReverse = true;
        }

        #region Display Interface

        public void Play()
        {
            /// Works from Forward and Stop
            SetDispatchSpeed(PlaybackSpeedConstants.NormalSpeed);
            StartDispatcher();

            //From rewind to play 
            if (isReverse)
            {
                isReverse = false;
                frameRiterator.RepositionIterator(ref frameIterator, currentFrame);
            }
            
        }

       
        
        public void StopPlayBack()
        {
            //From Play to Stop is safe
            ResetDispatcher();
            isPause = false;
        }

        public bool Pause
        {
            get
            {
                return isPause;
            }

            set
            {
                isPause = true;
                Stop();
            }
        }

        public void Forward()
        {
            //From Stop to Forward is a safe state
            SetDispatchSpeed(PlaybackSpeedConstants.FastSpeed);
            StartDispatcher();
        }

        public void Rewind()
        {
            //From Stop to Rewind is a safe state
            SetDispatchSpeed(64);
            ReverseIterator();
            StartDispatcher();
        }

        /// <summary>
        /// PlayBack Seeker Location
        /// </summary>
        public int Marker
        {
            get 
            {
                return currentIndex;
            }
        }

        #endregion
    }
}
