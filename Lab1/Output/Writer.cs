using System;

namespace Lab1.Output
{
    public class ConsoleWriter : IWriter
    {
        public void WriteLine(object s)
        {
            Console.WriteLine(s);
        }
        public void Write(object s)
        {
            Console.Write(s);
        }

        public void WriteLine(string format, params object[] args)
        {
            try
            {
                Console.WriteLine(format, args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void Write(string format, params object[] args)
        {
            try
            {
                Console.Write(format, args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
