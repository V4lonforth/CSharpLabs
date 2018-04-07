using System;

namespace Lab1.Input
{
    public class Parser
    {
        private StringFormatChecker stringFormatChecker;

        public Parser()
        {
            stringFormatChecker = new StringFormatChecker();
        }

        public Exception ParseNumber(string s, out int number, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            Exception exception = stringFormatChecker.CheckNumberFormat(s, minValue, maxValue);
            if (exception == null)
                number = int.Parse(s);
            else
                number = 0;
            return exception;
        }

        public Exception ParseKey(string s, out char key)
        {
            Exception exception = stringFormatChecker.CheckKeyFormat(s);
            if (exception == null)
                key = s[0];
            else
                key = ' ';
            return exception;
        }

        public Exception ParseBlockState(string s, out BlockState state)
        {
            Exception exception = stringFormatChecker.CheckBlockStateFormat(s);
            if (exception == null)
                state = ConvertToBlockState(s[0]);
            else
                state = 0;
            return exception;
        }

        private BlockState ConvertToBlockState(char c)
        {
            switch (char.ToUpper(c))
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
