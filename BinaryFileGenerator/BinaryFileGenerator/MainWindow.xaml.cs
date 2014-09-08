using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace BinaryFileGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string count = textbox11.Text;
            string sync = textbox12.Text;
            string timestamp = textbox13.Text;
            string status = textboxSK1.Text;
            string badframe = textboxSK2.Text;
            string sk3 = textboxSK3.Text;
            string sk4 = textboxSK4.Text;
            string sk5 = textboxSK5.Text;
            string sk6 = textboxSK6.Text;
            string sk7 = textboxSK7.Text;
            string sk8 = textboxSK8.Text;
            string sk9 = textboxSK9.Text;
            string sk10 = textboxSK10.Text;
            string sk11 = textboxSK11.Text;
            string sk12 = textboxSK12.Text;
            string sk13 = textboxSK13.Text;
            string sk14 = textboxSK14.Text;
            string sk15 = textboxSK15.Text;
            string sk16 = textboxSK16.Text;
            string sk17 = textboxSK17.Text;
            string sk18 = textboxSK18.Text;
            string pixels = textboxPixels.Text;
            string reserved1 = textboxSK1R.Text;
            string reserved2 = textboxSK2R.Text;
            string padding = textboxSKPadding.Text;
            string start = textboxStart.Text;
            string stop = textboxStop.Text;
            string sksize = textboxSKSize.Text;
            string window = textboxSKWindow.Text;
            string mode = textboxSKMode.Text;
            string irig = textboxSKIRIG.Text;

            //http://msdn.microsoft.com/en-us/library/swz6z5ks.aspx
            //http://stackoverflow.com/questions/1318933/c-sharp-int-to-byte
            //http://www.dotnetperls.com/int-parse
            StringBuilder frame = new StringBuilder();
            frame.Append(count);
            frame.Append(sync);
            frame.Append(timestamp);
            frame.Append(status);
            frame.Append(badframe);
            frame.Append(sk3);
            frame.Append(sk4);
            frame.Append(sk5);
            frame.Append(sk6);
            frame.Append(sk7);
            frame.Append(sk8);
            frame.Append(sk9);
            frame.Append(sk10);
            frame.Append(sk11);
            frame.Append(sk12);
            frame.Append(sk13);
            frame.Append(sk14);
            frame.Append(sk15);
            frame.Append(sk16);
            frame.Append(sk17);
            frame.Append(sk18);
            frame.Append(pixels);
            frame.Append(reserved1);
            frame.Append(reserved2);
            frame.Append(padding);
            frame.Append(start);
            frame.Append(stop);
            frame.Append(sksize);
            frame.Append(window);
            frame.Append(mode);
            frame.Append(irig);
            frame.Append("200");
            frame.Append("Version 1.4");

            int framesize =Encoding.ASCII.GetByteCount(frame.ToString().ToCharArray());
            Console.Out.WriteLine("framesize: {0}", framesize);
        }

    }
}
