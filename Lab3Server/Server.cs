using Lab3;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Lab3Server
{
    class Server
    {
        private const string localAddress = "127.0.0.1";
        private const int listeningPort = 2345;
        private const int listeningConnectionPort = 2346;
        private const int sendingPort = 5432;

        static void Main(string[] args)
        {
            Console.WriteLine("Lab3 server");

            ClientConnection connection = null;

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(new IPEndPoint(IPAddress.Parse(localAddress), listeningPort));

            Socket connectionSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            connectionSocket.Bind(new IPEndPoint(IPAddress.Parse(localAddress), listeningConnectionPort));
            Listener connectionListener = new Listener(connectionSocket, sendingPort, listeningConnectionPort);

            bool serverClosed = false;
            while (!serverClosed)
            {
                if (connection == null)
                {
                    connection = new ClientConnection(socket);
                    if (connection.Connect(connectionListener))
                    {
                        Thread connectionThread = new Thread(connection.Update);
                        connectionThread.Start();
                        connection = null;
                    }
                }
            }
        }
    }
}
