namespace Lab1.Output
{
    public interface IWriter
    {
        void WriteLine(object s);
        void Write(object s);
        void WriteLine(string format, params object[] args);
        void Write(string format, params object[] args);
    }
}
