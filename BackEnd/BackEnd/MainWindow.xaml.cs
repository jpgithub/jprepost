using BackEnd.Backend;
using System;
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
        private bool isReserve;
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

        }

        private void frame_Tick(object sender, EventArgs e)
        {
            FrameTime.Content = "Frame " + count.ToString();
            
            if (isReserve)
                count--; // if Frame.First() == begin call stop
            else
                count++;
            
            if ((count > 25) || (count < -25))
                count = 0;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Content = DateTime.Now.ToLongTimeString();
        }

        private void rewindBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!isSingleStep)
            {
                isReserve = true;
                frameInterval = 32;
                StartDispatcher();
            }

            if (isSingleStep)
            {
                count--;
                FrameTime.Content = "Frame " + count.ToString();
            }
        }
        
        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            frameInterval = 32;
            isReserve = false;
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
                isReserve = false;
                frameInterval /= 2;
                StartDispatcher();
            }

            if (isSingleStep)
            {
                count++;
                FrameTime.Content = "Frame " + count.ToString();
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
            isReserve = false;
            //DisplayOutput(Frame.First()); 
            count = 0;
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
    }
}
