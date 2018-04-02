using System;

namespace Lab1.Input
{
    public class Reader
    {
        private StringFormatChecker stringFormatChecker;

        public Reader()
        {
            stringFormatChecker = new StringFormatChecker();
        }

        private string ReadString(Func<string, int, int, Exception> func, int min, int max)
        {
            string str = null;
            bool isReadingCompleted = false;

            while (!isReadingCompleted)
            {
                str = Console.ReadLine();
                Exception exception = func(str, min, max);
                if (exception == null)
                    isReadingCompleted = true;
                else
                    Console.WriteLine(exception.Message + " Try again: ");
            }
            return str;
        }
        private string ReadString(Func<string, Exception> func)
        {
            string str = null;
            bool isReadingCompleted = false;

            while (!isReadingCompleted)
            {
                str = Console.ReadLine();
                Exception exception = func(str);
                if (exception == null)
                    isReadingCompleted = true;
                else
                    Console.WriteLine(exception.Message + " Try again: ");
            }
            return str;
        }

        public string ReadString(string s)
        {
            Console.Write(s);
            return Console.ReadLine();
        }

        public int ReadNumber(string s, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            Console.Write(s);
            return int.Parse(ReadString(stringFormatChecker.CheckNumberFormat, minValue, maxValue));
        }

        public char ReadKey(string s)
        {
            Console.Write(s);
            return ReadString(stringFormatChecker.CheckKeyFormat)[0];
        }

        public BlockState ReadState(string s)
        {
            Console.Write(s);
            string str = ReadString(stringFormatChecker.CheckBlockStateFormat);
            return ConvertToBlockState(str[0]);
        }
        public BlockState ConvertToBlockState(char c)
        {
            switch(char.ToUpper(c))
            {
                case 'R':
                    return BlockState.Red;
                case 'G':
                    return BlockState.Green;
                default:
                    return BlockState.Yellow;
            }
        }
    }
}
