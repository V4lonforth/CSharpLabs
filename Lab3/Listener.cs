using System;
using System.Net;
using System.Net.Sockets;
using Lab3.Input;

namespace Lab3
{
    public class Listener
    {
        private Socket socket;
        private Parser parser;

        public EndPoint sendingIP;
        public EndPoint listeningIP;

        private const int maxDataSize = 256;

        public Listener(Socket socket, int sendingPort, int listeningPort, string remoteAdress = null)
        {
            parser = new Parser(this);
            this.socket = socket;

            if (remoteAdress == null)
            {
                sendingIP = new IPEndPoint(IPAddress.Any, sendingPort);
                listeningIP = new IPEndPoint(IPAddress.Any, listeningPort);
            }
            else
            {
                listeningIP = new IPEndPoint(IPAddress.Parse(remoteAdress), listeningPort);
                sendingIP = new IPEndPoint(IPAddress.Parse(remoteAdress), sendingPort);
            }
        }

        public Exception Receive(out string s, out MessageStatus status)
        {
            bool received = false;
            byte[] bufData = null;
            MessageStatus messageStatus = MessageStatus.Error;
            while (!received)
            {
                if (socket.Available > 0)
                {
                    byte[] buf = new byte[maxDataSize];
                    try
                    {
                        int bytesCount = socket.ReceiveFrom(buf, ref listeningIP);
                    }
                    catch (Exception exception)
                    {
                        s = null;
                        status = messageStatus;
                        return exception;
                    }

                    int dataSize = BitConverter.ToInt32(buf, 0);
                    bufData = new byte[dataSize];
                    messageStatus = (MessageStatus)BitConverter.ToInt32(buf, sizeof(int));
                    Array.Copy(buf, sizeof(int) * 2, bufData, 0, dataSize);

                    received = true;
                }
            }
            s = parser.DecodeString(bufData);
            status = messageStatus;
            return null;
        }

        public Exception Send(string s, MessageStatus status = MessageStatus.OK)
        {
            byte[] buf = parser.EncodeString(s);
            byte[] data = new byte[buf.Length + sizeof(int) * 2];

            Array.Copy(BitConverter.GetBytes(buf.Length), data, sizeof(int));
            Array.Copy(BitConverter.GetBytes((int)status), 0, data, sizeof(int), sizeof(int));
            Array.Copy(buf, 0, data, sizeof(int) * 2, buf.Length);

            try
            {
                socket.SendTo(data, sendingIP);
            }
            catch (Exception exception)
            {
                return exception;
            }
            return null;
        }
        
        public byte[] GetBytes(int[] args)
        {
            byte[] data = new byte[args.Length * sizeof(int)];
            Buffer.BlockCopy(args, 0, data, 0, data.Length);
            return data;
        }
    }
}
