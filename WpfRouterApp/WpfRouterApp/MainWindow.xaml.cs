using Microsoft.AspNetCore.SignalR.Client;
using RoutingEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WebHubDefinitonFWLib;

namespace WpfRouterApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RouterEngine rte;
        private object tslistlock = new object();
        private object enginelistlock = new object();
        private ObservableCollection<string> tslist = new ObservableCollection<string>();
        private ObservableCollection<string> enginelist = new ObservableCollection<string>();
        private readonly static HubConnection routeregineconnection = new HubConnectionBuilder().WithUrl(UrlStrings.HubUrls.RouterEngineHub).Build();
        private readonly static HubConnection rtclkconnection = new HubConnectionBuilder().WithUrl(UrlStrings.HubUrls.RTClockHub).Build();
        private readonly static HubConnection trafficconnection = new HubConnectionBuilder().WithUrl(UrlStrings.HubUrls.DeviceProbeHub).Build();

        public MainWindow()
        {
            InitializeComponent();
            
            rte = new RouterEngine();
            REngineStatus.ItemsSource = enginelist;
            TaskStatusWindow.ItemsSource = tslist;
            BindingOperations.EnableCollectionSynchronization(tslist, tslistlock);
            BindingOperations.EnableCollectionSynchronization(enginelist, enginelistlock);
        }

        private void reipcHandler(RouterMessage obj)
        {
            lock (tslistlock)
            {
                tslist.Add(string.Format(" Task ID: {0} Progess: {1}", obj.TaskID, obj.Progress));
            }
        }

        private void trafficHandler(TrafficMessages obj)
        {
            lock (enginelistlock)
            {
                if (!string.IsNullOrEmpty(obj.External))
                {
                    enginelist.Add(obj.External);
                }

                if (!string.IsNullOrEmpty(obj.Internal))
                {
                    enginelist.Add(obj.Internal);
                }
            }

        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            rte.StartEngine(string.Empty);
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            rte.StopEngine();
        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            StartRTClkConnection();
            StartTrafficHubConnection();
            InitREHubConnection();
            JoinGroupConnection();
        }

        private async void InitREHubConnection()
        {
            routeregineconnection.On<RouterMessage>(UrlStrings.Events.RouterIPCSend, this.reipcHandler);
            try
            {
                await routeregineconnection.StartAsync();

                btn1.IsEnabled = true;
                btn2.IsEnabled = true;

            }
            catch
            {
                ;
            }
        }

        private async void Btn4_Click(object sender, RoutedEventArgs e)
        {
            rte.StopEngine();
            try
            {
                await routeregineconnection.SendAsync("RemoveFromGroup", UrlStrings.ReIPCGroupName);

            }
            catch
            {

            }

            CloseREHubConnection();
            btn1.IsEnabled = false;
            btn2.IsEnabled = false;
        }

        private async void StartRTClkConnection()
        {
            rtclkconnection.On<DateTime>(UrlStrings.Events.TimeSent,
                (time) =>
                {
                    this.Dispatcher.Invoke(() =>
                                {
                                    rtimebox.Text = time.ToLongTimeString();
                                });
                });
            try
            {
                await rtclkconnection.StartAsync();
            }
            catch
            {
                ;
            }
        }

        private async void StartTrafficHubConnection()
        {
            trafficconnection.On<TrafficMessages>(UrlStrings.Events.TrafficMsgSend, trafficHandler);
            try
            {
                await trafficconnection.StartAsync();
            }
            catch
            {

            }
        }

        private async void StopRTClkConnection()
        {
            try
            {
                await rtclkconnection.StopAsync();
            }
            catch
            {

            }
        }

        private async void CloseREHubConnection()
        {
            try
            {
                await routeregineconnection.StopAsync();
            }
            catch
            {

            }
        }

        private async void JoinGroupConnection()
        {
            try
            {
                await routeregineconnection.SendAsync("AddToGroup", UrlStrings.ReIPCGroupName);
            }
            catch
            {

            }
        }
    }
}
