using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Client
{
    public class Program
    {
        private const int portNum = 8080;
        private static readonly string hostName = IPAddress.Loopback.ToString();
        private static readonly string cmd_disconnect = "-999";
        public static int Main(String[] args)
        {
            Console.Out.WriteLine("Welcome to AppService Client");
            Random rand = new Random();
            int behavior = (rand.Next() % 10) + 1;
            Timer timer = new Timer();
            ushort countdown = ushort.MinValue;
            timer.Interval = 10000;
            timer.AutoReset = false;
            timer.Elapsed += (o, e) => { countdown = ushort.MaxValue; }; // Trigger abrupt shutdown
            timer.Start();
            Console.Out.WriteLine("Behavior: {0}", behavior);

            TcpClient client;

            try
            {
                client = new TcpClient(hostName, portNum);
                NetworkStream ns = client.GetStream();
                byte[] byteBuffer;
                // Polling the time
                while (ushort.MaxValue > countdown)
                {
                    byteBuffer = new byte[1024];
                    int bytesRead = ns.Read(byteBuffer, 0, byteBuffer.Length);
                    Console.WriteLine("\n" + Encoding.ASCII.GetString(byteBuffer, 0, bytesRead).Trim());    
                    
                    
                    // Trigger clean shutdown base on a number between 1 - 10
                    if (behavior > 3)
                    {
                        byteBuffer = Encoding.ASCII.GetBytes(cmd_disconnect);
                        ns.Write(byteBuffer,0,byteBuffer.Length);
                        ns.Flush();
                        behavior = -1;
                    }
                    else if (!ns.DataAvailable)
                    {
                        // Server has close the connection
                        break;
                    }
                    else
                        System.Threading.Thread.Sleep(1000); // Trigger client abrupt 
                   
                }
                //Console.In.ReadLine();// Could use a acknowledge from server to close the connect
               
                ns.Close();                
                client.Close();
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.ToString());
            }
            Console.In.ReadLine();
            return 0;
        }

    }
}
