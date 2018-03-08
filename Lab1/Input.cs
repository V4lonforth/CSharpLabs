using System;

namespace Lab1
{
    public static class Input
    {
        public static char ReadKey(string s)
        {
            Console.Write(s);
            string str = Console.ReadLine();
            while (str.Length > 1)
            {
                Console.Write("Expected 1 symbol. Try again: ");
                str = Console.ReadLine();
            }
            return str[0];
        }
        public static BlockState ReadState(string s)
        {
            while (true)
            {
                switch (char.ToUpper(ReadKey(s)))
                {
                    case 'R':
                        return BlockState.Red;
                    case 'G':
                        return BlockState.Green;
                    case 'Y':
                        return BlockState.Yellow;
                    default:
                        Console.Write("Wrong state. Try again: ");
                        continue;
                }
            }
        }
        public static int ReadNumber(string s, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            Console.Write(s);
            int number;
            while (true)
            {
                try
                {
                    number = Convert.ToInt32(Console.ReadLine());
                    if (number < minValue)
                        throw new Exception("Number should be no less than " + minValue.ToString() + ".");
                    if (number > maxValue)
                        throw new Exception("Number should be no greater than " + maxValue.ToString() + ".");
                    break;
                }
                catch (Exception e)
                {
                    Console.Write("{0} Try again: ", e.Message);
                }
            }
            return number;
        }
    }
}
