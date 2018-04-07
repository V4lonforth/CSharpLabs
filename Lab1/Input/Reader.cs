using System;

namespace Lab1.Input
{
    public class Reader
    {
        private Parser parser;

        public Reader()
        {
            parser = new Parser();
        }

        public string ReadString(string s)
        {
            Console.Write(s);
            return Console.ReadLine();
        }

        public int ReadNumber(string s, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            bool parsed = false;
            Console.Write(s);
            int number = 0;
            while (!parsed)
            {
                s = Console.ReadLine();
                Exception exception = parser.ParseNumber(s, out number, minValue, maxValue);
                if (exception == null)
                    parsed = true;
                else
                    Console.Write(exception.Message + " Try again: ");
            }
            return number;
        }

        public char ReadKey(string s)
        {
            bool parsed = false;
            Console.Write(s);
            char key = ' ';
            while (!parsed)
            {
                s = Console.ReadLine();
                Exception exception = parser.ParseKey(s, out key);
                if (exception == null)
                    parsed = true;
                else
                    Console.Write(exception.Message + " Try again: ");
            }
            return key;
        }

        public BlockState ReadState(string s)
        {

            bool parsed = false;
            Console.Write(s);
            BlockState state = 0;
            while (!parsed)
            {
                s = Console.ReadLine();
                Exception exception = parser.ParseBlockState(s, out state);
                if (exception == null)
                    parsed = true;
                else
                    Console.Write(exception.Message + " Try again: ");
            }
            return state;
        }
    }
}
