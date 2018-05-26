using System;
using System.ServiceModel;
using System.Threading;
using Lab4;

namespace Lab4Server
{
    class Server
    {
        static bool closed = false;

        static void Main(string[] args)
        {
            Thread thread = new Thread(Connect);
            thread.Start();

            while (!closed)
            {
                if (Connection.connected)
                {
                    Connection.connected = false;
                    ClientConnection clientConnection = new ClientConnection(Connection.id);
                    thread = new Thread(clientConnection.Update);
                    thread.Start();
                }
            }
        }
        private static void Connect()
        {
            using (ServiceHost host = new ServiceHost(typeof(Connection), new Uri("net.pipe://localhost")))
            {
                NetNamedPipeBinding netNamedPipeBinding = new NetNamedPipeBinding();

                host.AddServiceEndpoint(typeof(IConnection), new NetNamedPipeBinding(), "connection");
                host.Open();
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                closed = true;
                host.Close();
            }
        }
    }
}
