using System;
using Lab1;
using Lab2.Menu;
using Lab3;
using Lab3.Output;
using Lab3.Input;
using System.Net;
using System.Net.Sockets;

namespace Lab3Server
{
    class ClientConnection
    {
        private const int listeningPort = 2345;
        private const int sendingPort = 5432;

        private Listener listener;
        private UDPWriter writer;
        private UDPReader reader;

        private AdvancedMenuController menuController;

        public ClientConnection(Socket socket)
        {
            listener = new Listener(socket, sendingPort, listeningPort, "127.0.0.1");

            writer = new UDPWriter(listener);
            reader = new UDPReader(listener, writer);

            IAdvancedMenuActions menuActions = new AdvancedMenuActions(new PathController(reader), reader, writer);
            menuController = new AdvancedMenuController(menuActions, reader, writer);
        }

        public bool Connect(Listener listener)
        {
            UDPWriter writer = new UDPWriter(listener);
            UDPReader reader = new UDPReader(listener, writer);

            listener.Receive(out string s, out MessageStatus status);
            if (status == MessageStatus.OK)
            {
                listener.sendingIP = new IPEndPoint(IPAddress.Parse(s), sendingPort);
                listener.listeningIP = new IPEndPoint(IPAddress.Parse(s), listeningPort);
            }
            else
                return false;

            return true;
        }

        public void Update()
        {
            bool finished = false;
            while (!finished)
                finished = !menuController.PressKey();

            listener.Send("Connection closed.", MessageStatus.Exit);
        }
    }
}
