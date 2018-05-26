using System.Text;

namespace Lab3.Input
{
    public class Parser : Lab1.Input.Parser
    {
        private Listener listener;

        public Parser(Listener listener)
        {
            this.listener = listener;
        }
        
        public string DecodeString(byte[] bytes)
        {
            return Encoding.Unicode.GetString(bytes);
        }
        public byte[] EncodeString(string str)
        {
            return Encoding.Unicode.GetBytes(str);
        }
    }
}
