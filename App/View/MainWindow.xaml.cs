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

using WpfApplication2.ViewModel;

namespace WpfApplication2.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ImgViewModel();
            ImageTime.Text = "This TextBox Belongs to " + ImageTime.Dispatcher.Thread.Name;

            
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
             ImgViewModel mvm = this.DataContext as ImgViewModel;
             mvm.ImageLocation = e.GetPosition(this).ToString();
             
        }

        private void formatMarginsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Instantiate the dialog box
            DialogBox dlgView = new DialogBox();

            // Call parent view model to get child view model
            ImgViewModel mvm = this.DataContext as ImgViewModel;
            // Configure the dialog box
            dlgView.DataContext = mvm.DlgViewModel ;
            
            // Open the dialog box modally 
            dlgView.ShowDialog();   
        }



        
    }
}
