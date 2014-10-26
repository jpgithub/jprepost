using BackEnd.Backend;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BackEnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int count = 0;
        private DispatcherTimer frametimer = new DispatcherTimer();
        private double frameInterval = 32;
        private DispatcherTimer timer;
        private List<FrameObject> Frames = new List<FrameObject>();
        private bool isReverse;
        private bool isSingleStep;
        private bool isPause;
        public delegate void QueryTime();
        
        
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            frametimer.Interval = TimeSpan.FromMilliseconds(32);
            frametimer.Tick += frame_Tick;

            //frametimer.Start();
            //FrameObject test = new FrameObject();
            //byte[] teststream = { 0xff,0x00,0xff,0xff,0xff,0x00,0x00 };
            //FrameParser.SvaFrameParser((uint)FrameObject.Versions.V14, teststream, out test);
            //new TctFileReader(string.Empty);
            //Frames.Add(test);

            count = 0;
            this.InitializeFrames();
            count = (int)(uint.MinValue);
            
            
        }

        private void frame_Tick(object sender, EventArgs e)
        {
            if (isReverse)
            {
                count--; // if Frame.First() == begin call stop
            }
            else
            {
                count++;
            }

            GetAFrame();
        }

        private void GetAFrame()
        {
            if ((count < uint.MinValue) || (count >= Frames.Count))
            {
                count = (int)uint.MinValue;
                frametimer.Stop();
            }

            FrameTime.Content = "Frame " + Frames[count].FieldOne.ToString();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Content = DateTime.Now.ToLongTimeString();
        }

        private void rewindBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!isSingleStep)
            {
                isReverse = true;
                frameInterval = 32;
                StartDispatcher();
            }

            if (isSingleStep)
            {
                count--;
                GetAFrame();
            }
        }
        
        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            frameInterval = 32;
            isReverse = false;
            StartDispatcher();  
        }

        private void StartDispatcher()
        {
            if (!frametimer.IsEnabled)
            {
                frametimer.Start();
            }
            frametimer.Interval = TimeSpan.FromMilliseconds(frameInterval);
        }

        private void CheckTime()
        {
            if (!timer.IsEnabled)
            {
                timer.Start();
            }
        }

        private void forwardBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!isSingleStep)
            {
                isReverse = false;
                frameInterval /= 2;
                StartDispatcher();
            }

            if (isSingleStep)
            {
                count++;
                GetAFrame();
            }
        }

        private void pauseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!isPause)
            {
                frametimer.Stop();
                isPause = true;
            }
            else
            {
                isPause = false;
                StartDispatcher();
            }
        }

        private void stopBtn_Click(object sender, RoutedEventArgs e)
        {
            frametimer.Stop();
            isReverse = false;
            //DisplayOutput(Frame.First()); 
           
            FrameObject  begin = Frames.First();
            count = (int)begin.FieldOne;
            FrameTime.Content = "Frame " + count.ToString();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            isSingleStep = true;
            playBtn.IsEnabled = false;
            stopBtn.IsEnabled = false;
            pauseBtn.IsEnabled = false;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            isSingleStep = false;
            playBtn.IsEnabled = true;
            stopBtn.IsEnabled = true;
            pauseBtn.IsEnabled = true;
        }

        private void InitializeFrames()
        {
            while (count <= 200)
            {
                FrameObject frame = new FrameObject();
                frame.FieldOne = (uint)count;
                Frames.Add(frame);
                count++;
            }
        }

        
    }
}
