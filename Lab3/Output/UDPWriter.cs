using System.Net;
using Lab1.Output;

namespace Lab3.Output
{
    public class UDPWriter : IWriter
    {
        public EndPoint RemoteIp { set; private get; }
        private Listener listener;

        public UDPWriter(Listener listener)
        {
            this.listener = listener;
        }

        public void Write(object s)
        {
            Write(MessageStatus.OK, s);
        }
        public void Write(MessageStatus status, object s)
        {
            listener.Send(s.ToString(), status);
        }

        public void Write(string format, params object[] args)
        {
            Write(MessageStatus.OK, format, args);
        }
        public void Write(MessageStatus status, string format, params object[] args)
        {
            listener.Send(string.Format(format, args), status);
        }

        public void WriteLine(object s)
        {
            WriteLine(MessageStatus.OK, s);
        }
        public void WriteLine(MessageStatus status, object s)
        {
            listener.Send(s.ToString() + '\n', status);
        }

        public void WriteLine(string format, params object[] args)
        {
            WriteLine(MessageStatus.OK, format, args);
        }
        public void WriteLine(MessageStatus status, string format, params object[] args)
        {
            listener.Send(string.Format(format, args) + '\n', status);
        }
    }
}
