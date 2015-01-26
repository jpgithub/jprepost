using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppService
{
    public class Program
    {
        //  Accept Clients Async
        private const int portNum = 8080;
        
        public static int Main(String[] args)
        {
            bool done = false;
            Console.WriteLine("Welcome to AppService Server");

            TcpListener serverInstance = new TcpListener(IPAddress.Loopback, portNum);


            // Start Server
            serverInstance.Start(100);

            uint counter = uint.MinValue;

            List<Subscriber> clientList = new List<Subscriber>();
            
            // Will only loop here
            while (!done)
            {
               Console.WriteLine("Waiting for connection...");

               Subscriber newClient = new Subscriber(serverInstance.AcceptTcpClient(), counter++);

               new Thread ( new ThreadStart( newClient.ProcessRequest ) ).Start();

               //Console.WriteLine("Client {0}", counter++);
               // AcceptClientAsync(serverInstance);
            }

            serverInstance.Stop();

            return 0;
        }
        /*
        private void AcceptClientSync(TcpClient client)
        {
            Subscriber newClient = new Subscriber(client);
        }

        // This is a concurreny is slow at accepting client because it maybe work of the same thread, second client is queued 
        private static async void AcceptClientAsync(TcpListener serverInstance)
        {
            Subscriber client;
            try
            {

                TcpClient tcpClient =  await serverInstance.AcceptTcpClientAsync();
                if (tcpClient.Connected)
                {
                    client = new Subscriber(tcpClient);
                }

                //client = new Subscriber(serverInstance.AcceptTcpClient());

                //Console.WriteLine("Connection accepted.");

                //counter++;

                //Console.WriteLine("Got {0} client", counter);
                //ns.Write(byteTime, 0, byteTime.Length);
                //ns.Close();
                //clientNS.Close();
                //client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        */
    }

    
    internal class Subscriber
    {
        private TcpClient clientHandler;
        private byte[] ReadBuffer;
        private byte[] WriteBuffer;
        private CancellationTokenSource cancelWrite;
        private uint subId;

        public Subscriber(TcpClient client, uint clientNumber)
        {
            this.clientHandler = client;
            this.ReadBuffer = new byte[256];
            this.cancelWrite = new CancellationTokenSource();
            this.subId = clientNumber;
        }

        public void ProcessRequest()
        {
           using (NetworkStream clientNS  = clientHandler.GetStream())
           {
               try
               {
                   while (clientNS.CanRead && clientNS.CanWrite)
                   {

                       WriteBuffer = Encoding.ASCII.GetBytes("\nSending Current Time:" + DateTime.Now.ToString());
                       clientNS.WriteAsync(WriteBuffer, 0, WriteBuffer.Length, cancelWrite.Token);
                       if (clientNS.DataAvailable)
                       {
                           Console.WriteLine("\n There is data in read buffer, could be an incoming request");

                           int bytesRead = clientNS.Read(ReadBuffer, 0, ReadBuffer.Length);
                           if (bytesRead > 0)
                           {
                               string incomingRequest = Encoding.ASCII.GetString(ReadBuffer, 0, bytesRead);

                               Console.WriteLine(incomingRequest);

                               if (int.Parse(incomingRequest) == -999)
                               {
                                   cancelWrite.Cancel();
                                   break;
                               }
                           }
                       }
                   }
                   
               }
               catch (IOException e)
               {
                   // remote client shutdown or abruptly closed without sending proper shutdown command
                   cancelWrite.Cancel();
                   Console.Error.WriteLine("\n" + e.Message);
               }
           }

           clientHandler.Close();
           Console.Out.WriteLine("Subscriber {0} disconnected", subId);  
        }

        private void myWriteCallback(IAsyncResult ar)
        {

            //Get the network that handles the client request
            NetworkStream handler = (NetworkStream)ar.AsyncState;

            byte[] byteTime = Encoding.ASCII.GetBytes(DateTime.Now.ToString());

            // Need to accept cancel token from read
            //handler.WriteAsync(byteTime, 0, byteTime.Length,cancelWrite.Token);

            handler.BeginRead(ReadBuffer, 0, ReadBuffer.Length, new AsyncCallback(myReadCallback), handler);
            
        }

        private void myReadCallback(IAsyncResult ar)
        {
            NetworkStream handler = (NetworkStream)ar.AsyncState;

            try
            {
                int bytesRead = handler.EndRead(ar);


                if (bytesRead > 0)
                {
                    string incomingRequest = Encoding.ASCII.GetString(ReadBuffer, 0, bytesRead);

                    Console.WriteLine(incomingRequest);

                    if (int.Parse(incomingRequest) == -999)
                    {
                        cancelWrite.Cancel();
                        handler.EndWrite(ar);
                        handler.Close();
                        clientHandler.Close();
                    }
                    else
                    {
                        // Not all data received. Get more.
                        handler.BeginRead(ReadBuffer, 0, ReadBuffer.Length, new AsyncCallback(myReadCallback), handler);
                    }

                }

            }
            catch (IOException e)
            {
                // remote client shutdown or abruptly closed without sending proper shutdown command
                cancelWrite.Cancel();
                Console.Error.WriteLine("\n" + e.Message);
            }
            finally
            {
                handler.Close();
                clientHandler.Close();
            }
            
        }
    }
}
