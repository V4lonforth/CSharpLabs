using System;
using System.Collections.Generic;
using Lab1.Input;
using Lab1.Output;

namespace Lab1.Menu
{
    public class MenuController
    {
        private IReader reader;
        private IWriter writer;

        private bool shouldExit;

        protected List<MenuElement> menuElements;
        
        public MenuController(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }
        public MenuController(IMenuActions actions, IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
            FillMenuElements(actions);
        }
        public MenuController(List<MenuElement> elements, IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
            menuElements = elements;
        }

        protected void FillMenuElements(IMenuActions actions)
        {
            menuElements = new List<MenuElement>
            {
                new MenuElement("Check", actions.Check),
                new MenuElement("View state", actions.ViewState),
                new MenuElement("Change signal", actions.ChangeState),
                new MenuElement("Exit", () => shouldExit = true)
            };
        }

        public bool PressKey()
        {
            int number = reader.ReadNumber(GetMenuText() + ">", 1, menuElements.Count);
            menuElements[number - 1].PressKey();

            if (shouldExit)
                return false;
            return true;
        }

        private string GetMenuText()
        {
            string s = "\nMenu:\n";
            for (int i = 0; i < menuElements.Count; i++)
                s += menuElements[i].WriteElementText(i + 1, writer) + '\n';
            return s;
        }
    }
}