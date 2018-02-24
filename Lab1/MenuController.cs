using System;
using System.Collections.Generic;

namespace Lab1
{
    public class MenuController
    {
        protected List<MenuElement> menuElements;
        protected PathController pathController;

        private bool shouldExit;

        public MenuController()
        {
            pathController = new PathController();
            pathController.ReadBlocksInfo();
            FillMenuElements();
        }
        public MenuController(PathController controller)
        {
            pathController = controller;
            FillMenuElements();
        }
        public MenuController(PathController controller, List<MenuElement> elements)
        {
            menuElements = elements;
            pathController = controller;
        }

        protected void FillMenuElements()
        {
            menuElements = new List<MenuElement> {
                new MenuElement("Check", pathController.Check),
                new MenuElement("View state", pathController.ViewState),
                new MenuElement("Change signal", pathController.ChangeState),
                new MenuElement("Exit", () => shouldExit = true)
            };
        }

        public bool PressKey()
        {
            int number = Input.ReadNumber(">", 1, menuElements.Count);
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