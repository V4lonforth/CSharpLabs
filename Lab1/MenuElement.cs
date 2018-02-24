using System;

namespace Lab1
{ 
    public class MenuElement
    {
        private Action action;
        private string text;

        public MenuElement(string text, Action action)
        {
            this.text = text;
            this.action = action;
        }

        public void PressKey()
        {
            action();
        }

        public void WriteElementText(int index)
        {
            Console.WriteLine("{0}.{1}", index, text);
        }
    }
}