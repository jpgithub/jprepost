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
using System.Windows.Shapes;
using AppServer.ViewModel;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace AppServer.View
{
    /// <summary>
    /// Interaction logic for DialogBox.xaml
    /// </summary>
    public partial class ServerConsole : Window
    {
        public ServerConsole()
        {
            InitializeComponent();
            DataContext = new ServerViewModel(this.BtnOpenFileDialog);
 
        }

        private void BtnOpenFileDialog()
        {
            // Configure save file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            //dlg.FileName = "Document"; // Default file name
            //dlg.DefaultExt = ".text"; // Default file extension
            //dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension 

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results 
            if (result == true)
            {
                // Save document 
                ServerViewModel mymodel = this.DataContext as ServerViewModel;
                mymodel.SetFileName = dlg.FileName;

            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
