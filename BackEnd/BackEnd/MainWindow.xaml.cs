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
        private uint count = 0;
        private DispatcherTimer frametimer = new DispatcherTimer();
        private double frameInterval = 32;
        private DispatcherTimer timer;
        private List<FrameObject> Frames = new List<FrameObject>();
            
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            //timer.Tick += frame_Tick;
            //timer.Start();

            frametimer.Interval = TimeSpan.FromMilliseconds(32);
            frametimer.Tick += frame_Tick;
            //frametimer.Start();
            FrameObject test = new FrameObject();

            byte[] teststream = { 0xff,0x00,0xff,0xff,0xff,0x00,0x00 };

            FrameParser.SvaFrameParser((uint)FrameObject.Versions.V14, teststream, out test);

            new TctFileReader(string.Empty);

            Frames.Add(test);

        }

        private void frame_Tick(object sender, EventArgs e)
        {
            FrameTime.Content = "Frame " + count.ToString();
            count++;
            if (count > 25)
                FrameSet.Content = count.ToString();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Content = DateTime.Now.ToLongTimeString();
            count = 1;
        }

        private void rewindBtn_Click(object sender, RoutedEventArgs e)
        {
            // Wrong Implementation
            frameInterval *= 2;
            frametimer.Interval = TimeSpan.FromMilliseconds(frameInterval); 
        }
        public delegate void QueryTime();
        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            playBtn.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new QueryTime(CheckTime));
            //if (!frametimer.IsEnabled)
            //    frametimer.Start();

            //frameInterval = 32;
            //frametimer.Interval = TimeSpan.FromMilliseconds(frameInterval);
   
        }

        private void CheckTime()
        {
            if (!timer.IsEnabled)
                timer.Start();
            
        }

        private void forwardBtn_Click(object sender, RoutedEventArgs e)
        {
            frameInterval /= 2;
            frametimer.Interval=TimeSpan.FromMilliseconds(frameInterval); 
        }

        private void pauseBtn_Click(object sender, RoutedEventArgs e)
        {
            frametimer.Stop();
        }


    }
}
