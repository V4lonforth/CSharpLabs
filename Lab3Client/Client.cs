using System;
using System.Threading;
using System.Globalization;
using System.Net;
using Lab1.Output;
using Lab1.Input;
using Lab3.Output;
using Lab3.Input;
using System.Net.Sockets;

namespace Lab3
{
    class Client
    {
        private static UDPReader reader;
        private static bool running = true;

        private static ConsoleReader consoleReader;
        private static ConsoleWriter consoleWriter;

        private const int listeningPort = 5432;
        private const int sendingPort = 2345;
        private const int sendingConnectionPort = 2346;
        private static DateTime dateTime;
        private static bool checking = false;

        private const string localAddress = "127.0.0.1";

        private static void Write()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
            while (running)
            {
                consoleWriter.Write(reader.ReadString(out MessageStatus status));
                if (status == MessageStatus.Exit)
                    running = false;
                checking = false;
            }
        }

        static void Check()
        {
            while (running)
            {
                if (checking && DateTime.Now.Subtract(dateTime).Seconds > 2)
                {
                    Console.WriteLine("Waiting time exceeded.");
                    running = false;
                }
            }
        }

        static void Main(string[] args)
        {
            consoleReader = new ConsoleReader();
            consoleWriter = new ConsoleWriter();

            consoleWriter.WriteLine("Lab3 client");
            IPAddress address;
            if (!IPAddress.TryParse(args[0], out address) || !IPAddress.TryParse(args[1], out address))
            {
                consoleWriter.WriteLine("Parsing error");
                return;
            }
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(new IPEndPoint(IPAddress.Parse(localAddress), listeningPort));
            Listener listener = new Listener(socket, sendingConnectionPort, listeningPort, args[0]);
            UDPWriter writer = new UDPWriter(listener);
            
            writer.Write(MessageStatus.OK, args[1]);
            checking = true;

            listener = new Listener(socket, sendingPort, listeningPort, args[0]);
            writer = new UDPWriter(listener);
            reader = new UDPReader(listener, writer);

            Thread thread = new Thread(Write);
            thread.Start();

            dateTime = DateTime.Now;
            Thread checkThread = new Thread(Check);
            checkThread.Start();

            while (running)
            {
                writer.Write(consoleReader.ReadString());
                checking = true;
                dateTime = DateTime.Now;
            }
        }
    }
}
