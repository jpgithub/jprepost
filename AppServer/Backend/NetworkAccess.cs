using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using AppServer.Model;
using AppServer.Misc;

namespace AppServer.Backend
{
    public class NetworkAccess : INetworkStatus
    {
        private ManualResetEvent allDone;
        private byte[] senddata;
        private int portNumber;
        private Socket listener;
        private MessageEventArgs status = new MessageEventArgs();
        public NetworkAccess(byte[] Data, int PortNum)
        {
            allDone = new ManualResetEvent(false);
            portNumber = PortNum;
            senddata = Data;
        }

        public void StartServer()
        {
            if (portNumber != int.MinValue)
            {
                InitalizeSocketAndStartListening();
            }
            else
            {
                OnNetworkStatusChanged(new ErrorEventArgs(new Exception("Invalid Port Number")));
            }

        }
        /// <summary>
        /// StopServer will Close Socket
        /// </summary>
        public void StopServer()
        {
            if (listener != null)
            {
                
                listener.Close();
                listener = null;
            }
            else
            {
                OnNetworkStatusChanged(new ErrorEventArgs(new Exception("Server is already offline")));
            }
        }

        private void InitalizeSocketAndStartListening()
        {   
            //Establish the local endpoint for the socket.
            //The DNS name of the computer
            //IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Loopback;
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, portNumber);
            
            // Create a UDP socket
            //listener = new Socket(localEndPoint.Address.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);
                //int n = 0;
                //Bind the socket to the local endpoint and listen for incoming connections.
                status.Message += "\nWaiting for Connection...";
                OnServerStatusChanged(status);
                    
                while (listener != null)
                {
                    allDone.Reset();
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
                    //n += listener.SendTo(senddata,0,senddata.Length/1040,SocketFlags.None,localEndPoint);
                    allDone.WaitOne();
                }
            }
            catch (SocketException e)
            {
                OnNetworkStatusChanged(new ErrorEventArgs(e));
            }
            
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            //Signal the main thread to continue
            allDone.Set();

            //Get the socket that handles the client request
            Socket clilistener = (Socket)ar.AsyncState;
            Socket handler = clilistener.EndAccept(ar);

            //Create the state object
            Send(handler, senddata);
            
        }

        private void ReadCallback(IAsyncResult ar)
        {
            ;
        }

        private void Send(Socket handler, byte[] data)
        {
            handler.BeginSend(data, 0, data.Length, 0, new AsyncCallback(SendCallback), handler);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                int bytesSent = handler.EndSend(ar);
                status.Message += "\nSent "+bytesSent.ToString()+" bytes to client";
                OnServerStatusChanged(status);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (SocketException e)
            {
                OnNetworkStatusChanged(new ErrorEventArgs(e));
            }
        }

        public event ErrorEventHandler NetworkStatus;

        protected void OnNetworkStatusChanged(ErrorEventArgs e)
        {
            ErrorEventHandler handler = NetworkStatus;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<MessageEventArgs> ServerStatus;

        protected void OnServerStatusChanged(MessageEventArgs e)
        {
            EventHandler<MessageEventArgs> handler = ServerStatus;
            if (handler != null)
            {
                handler(this, e);
            }
        }

    }
}
