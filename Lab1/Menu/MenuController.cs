using System;
using System.Collections.Generic;
using Lab1.Input;

namespace Lab1.Menu
{
    public class MenuController
    {
        private Reader reader;

        private bool shouldExit;

        protected List<MenuElement> menuElements;
        
        public MenuController()
        {
            reader = new Reader();
        }
        public MenuController(IMenuActions actions)
        {
            reader = new Reader();
            FillMenuElements(actions);
        }
        public MenuController(List<MenuElement> elements)
        {
            reader = new Reader();
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
            int number = reader.ReadNumber(">", 1, menuElements.Count);
            menuElements[number - 1].PressKey();

            if (shouldExit)
                return false;
            return true;
        }

        public void WriteMenuText()
        {
            Console.WriteLine("\nMenu:");
            for (int i = 0; i < menuElements.Count; i++)
                menuElements[i].WriteElementText(i + 1);
        }
    }
}