using System;
using Lab1.Output;

namespace Lab1.Menu
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

        public string WriteElementText(int index, IWriter writer)
        {
            return index.ToString() + '.' + text;
        }
    }
}