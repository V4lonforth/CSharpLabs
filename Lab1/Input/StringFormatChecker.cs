using System;

namespace Lab1.Input
{
    public class StringFormatChecker
    {
        public Exception CheckNumberFormat(string s, int minValue, int maxValue)
        {
            try
            {
                int number = Convert.ToInt32(s);
                if (number < minValue)
                    return new Exception("Number should be greater than or equal to " + minValue.ToString() + ".");
                if (number > maxValue)
                    return new Exception("Number should be less than or equal to " + maxValue.ToString() + ".");
            }
            catch (Exception e)
            {
                return e;
            }
            return null;
        }

        public Exception CheckKeyFormat(string s)
        {
            if (s.Length != 1)
                return new Exception("Expected 1 symbol.");
            return null;
        }

        public Exception CheckBlockStateFormat(string s)
        {
            Exception e = CheckKeyFormat(s);
            if (e == null)
            {
                char c = char.ToUpper(s[0]);
                if (c == 'R' || c == 'G' || c == 'Y')
                    return null;
                else
                    return new Exception("Wrong state.");
            }
            else
                return e;
        }
    }
}
