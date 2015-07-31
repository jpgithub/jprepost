using System;
using System.Collections.Generic;
using System.IO;
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
using System.Collections;
using WpfApplication4.DataObject;
using WpfApplication4.Misc;
using Microsoft.Win32;

namespace WpfApplication4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<ATS> atslist = new List<ATS>();
        private List<TPS> tpslist;
        
        private ListCollectionView Filepaths;
        private TPS selectedTps;
        private string _workingdir;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            _workingdir = @"" + DirPath.Text;
			// Simple rename test snippet for code , please set break point
            if(_workingdir.Contains(".RAW"))
            {
                var newpath = _workingdir.Substring(0,_workingdir.Length - System.IO.Path.GetExtension(_workingdir).Length);
                var newPath = newpath + ".tel";
                File.Move(_workingdir, newPath);
            }
            //end
            try
            {
                if (Directory.Exists(_workingdir))
                {
                    Directory.SetCurrentDirectory(_workingdir);
                    int size = Directory.EnumerateDirectories(_workingdir).Count();
                    IEnumerator iter = Directory.EnumerateDirectories(_workingdir).GetEnumerator();

                    pbStatus.Value = (100/size);
                    while (iter.MoveNext())
                    {
                        pbStatus.Value += pbStatus.Value;

                        string dir = iter.Current as string;

                        // to factor '\' the slash
                        string ats = dir.Substring(_workingdir.Length+1);
                        string [] atsinfo = ats.Split(new char[] { '_' },StringSplitOptions.RemoveEmptyEntries);

                        // Might have to remove the prefix before proceed
                        int val = 0;
                        int.TryParse(atsinfo.ElementAtOrDefault(1), out val);

                        atslist.Add(  new ATS(){ Name = ats, ID = val, Date = atsinfo.ElementAtOrDefault(2), TimeStamp = atsinfo.ElementAtOrDefault(3) }  );
                    }

                    

                    //Sort ATS by Date then by Timestamp 
                    ATSComparer atscmp = new ATSComparer();
                    atslist.Sort(atscmp);

                    Task<List<TPS>> TpsListTask = GenerateTpsList();

                    tpslist = TpsListTask.Result;
                    atslist.Clear();   
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                // Configure the message box to be displayed
                string messageBoxText = ex.Message;
                string caption = "Error";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show(messageBoxText, caption, button, icon);

            }

            Filepaths = new ListCollectionView(tpslist);
            Filepaths.GroupDescriptions.Add(new PropertyGroupDescription("Date"));
            ListTPSView.ItemsSource = Filepaths;
            //pbStatus.Value = 0;
        }
        
        private Task<List<TPS>> GenerateTpsList()
        {
            // Generate TPS List
            return Task<List<TPS>>.Factory.StartNew(() =>
            {
                List<TPS> tpsList = new List<TPS>();
                int currentIndex = 0;
                int currentAtsdelimiter = atslist[currentIndex].ID;
                
                while (true)
                {
                    try
                    {
                        // find the index of next delimiter                    
                        int nextIndex = atslist.FindIndex(currentIndex + 1, item => item.ID == currentAtsdelimiter);
                        if (nextIndex > 0)
                        {
                            tpsList.Add(TPS.Create("TPS", atslist.GetRange(currentIndex, nextIndex - currentIndex)));
                            currentIndex = nextIndex;
                            currentAtsdelimiter = atslist[currentIndex].ID;

                        }
                        else
                        {
                            //End of List
                            tpsList.Add(TPS.Create("TPS", atslist.GetRange(currentIndex, atslist.Count - currentIndex)));
                            break;
                        }
                       
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        // Error
                        break;
                    }
                }

                return tpsList;
            });
        }

        
        private void ListTPSView_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var datagrid = sender as DataGrid;
            selectedTps = datagrid.SelectedItem as TPS;
        }

        /// <summary>
        /// Right Click Function For Data Grid View - a.k.a Context Menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Pop a Dialog box 
            // Fetch the DestDirName

            string destDirName = DesDirPath.Text;
            // create directory using selectedTps.Name + selectedTps.Date
            // transfer associated Ats with selected Tps to destination directory

            pbStatus.Value = (100 / selectedTps.ATSList.Count);

            if (destDirName == string.Empty)
            {
                // Configure the message box to be displayed
                string messageBoxText = "Missing Destination Directory";
                string caption = "Error";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show(messageBoxText, caption, button, icon);
                DesDirPath.Focus();
                pbStatus.Value = 0;
                return;
            }
           
            foreach (ATS ats in selectedTps.ATSList)
            {
                StringBuilder sourceDirLoc = new StringBuilder(_workingdir);
                sourceDirLoc.Append('\\');
                sourceDirLoc.Append(ats.Name);

                StringBuilder destDirLoc = new StringBuilder(destDirName);
                destDirLoc.Append('\\');
                destDirLoc.Append(ats.Name);

                pbStatus.Value += pbStatus.Value;
                try
                {
                    Task xcopytask = Task.Factory.StartNew(() =>
                        {
                            XCopy.DirectoryCopy(sourceDirLoc.ToString(), destDirLoc.ToString(), true);
                        });
                    xcopytask.Wait();
                }
                catch (AggregateException ex)
                {
                    // Configure the message box to be displayed
                    string messageBoxText = ex.InnerException.Message;
                    string caption = "Error";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Error;
                    MessageBox.Show(messageBoxText, caption, button, icon);
                }
                    
            }
        }
    }
}
