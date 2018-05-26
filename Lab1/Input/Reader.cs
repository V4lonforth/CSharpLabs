using System;
using Lab1.Output;

namespace Lab1.Input
{
    public class ConsoleReader : IReader
    {
        private Parser parser;
        private IWriter writer;

        public ConsoleReader()
        {
            parser = new Parser();
            writer = new ConsoleWriter();
        }

        public string ReadString(string s = null)
        {
            if (s != null)
                writer.Write(s);
            return Console.ReadLine();
        }

        public int ReadNumber(string s, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            bool parsed = false;
            writer.Write(s);
            int number = 0;
            while (!parsed)
            {
                s = Console.ReadLine();
                Exception exception = parser.ParseNumber(s, out number, minValue, maxValue);
                if (exception == null)
                    parsed = true;
                else
                    writer.Write(exception.Message + " Try again: ");
            }
            return number;
        }

        public char ReadKey(string s)
        {
            bool parsed = false;
            writer.Write(s);
            char key = ' ';
            while (!parsed)
            {
                s = Console.ReadLine();
                Exception exception = parser.ParseKey(s, out key);
                if (exception == null)
                    parsed = true;
                else
                    writer.Write(exception.Message + " Try again: ");
            }
            return key;
        }

        public BlockState ReadState(string s)
        {

            bool parsed = false;
            writer.Write(s);
            BlockState state = 0;
            while (!parsed)
            {
                s = Console.ReadLine();
                Exception exception = parser.ParseBlockState(s, out state);
                if (exception == null)
                    parsed = true;
                else
                    writer.Write(exception.Message + " Try again: ");
            }
            return state;
        }
    }
}
