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
        private uint count = 1;
        private DispatcherTimer frametimer = new DispatcherTimer();
        private double frameInterval = 32;
            
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            frametimer.Interval = TimeSpan.FromMilliseconds(32);
            frametimer.Tick += frame_Tick;
            frametimer.Start();

        }

        private void frame_Tick(object sender, EventArgs e)
        {
            FrameTime.Content = "Frame " + count.ToString();
            count++;
            if (count > 25)
                FrameSet.Content = count.ToString();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Content = DateTime.Now.ToLongTimeString();
            count = 1;
        }

        private void rewindBtn_Click(object sender, RoutedEventArgs e)
        {
            frameInterval *= 2;
            frametimer.Interval = TimeSpan.FromMilliseconds(frameInterval); 
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            frameInterval = 32;
            frametimer.Interval = TimeSpan.FromMilliseconds(frameInterval); 
        }

        private void forwardBtn_Click(object sender, RoutedEventArgs e)
        {
            frameInterval /= 2;
            frametimer.Interval=TimeSpan.FromMilliseconds(frameInterval); 
        }


    }
}
