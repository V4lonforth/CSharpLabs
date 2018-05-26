namespace Lab1.Input
{
    public interface IReader
    {
        string ReadString(string s);
        int ReadNumber(string s, int minValue, int maxValue);
        char ReadKey(string s);
        BlockState ReadState(string s);
    }
}
