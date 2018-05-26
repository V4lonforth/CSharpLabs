using System;
using Lab1;
using Lab1.Input;
using Lab1.Output;

namespace Lab3.Input
{
    public class UDPReader : IReader
    {
        private Listener listener;
        private IWriter writer;
        private Lab1.Input.Parser parser;

        public UDPReader(Listener listener, IWriter writer)
        {
            this.listener = listener;
            this.writer = writer;
            parser = new Lab1.Input.Parser();
        }

        public char ReadKey(string s)
        {
            char key = ' ';
            bool parsed = false;
            writer.Write(s);
            while (!parsed)
            {
                s = ReadString();
                Exception exception = parser.ParseKey(s, out key);
                if (exception == null)
                    parsed = true;
                else
                    writer.Write(exception.Message);
            }
            return key;
        }

        public int ReadNumber(string s, int minValue, int maxValue)
        {
            int number = 0;
            bool parsed = false;
            writer.Write(s);
            while (!parsed)
            {
                s = ReadString();
                Exception exception = parser.ParseNumber(s, out number, minValue, maxValue);
                if (exception == null)
                    parsed = true;
                else
                    writer.Write(exception.Message + " Try again: ");
            }
            return number;
        }

        public BlockState ReadState(string s)
        {
            BlockState state = 0;
            bool parsed = false;
            writer.Write(s);
            while (!parsed)
            {
                s = ReadString();
                Exception exception = parser.ParseBlockState(s, out state);
                if (exception == null)
                    parsed = true;
                else
                    writer.Write(exception.Message + " Try again: ");
            }
            return state;
        }

        public string ReadString()
        {
            return ReadString(out MessageStatus status);
        }

        public string ReadString(out MessageStatus status)
        {
            string s = null;
            bool received = false;
            status = MessageStatus.Error;
            while (!received)
            {
                Exception exception = listener.Receive(out s, out status);
                if (exception == null)
                    received = true;
                else
                    writer.WriteLine(exception.Message + " Try again: ");
            }
            return s;
        }
        public string ReadString(string s)
        {
            writer.Write(s);
            return ReadString();
        }
    }
}
