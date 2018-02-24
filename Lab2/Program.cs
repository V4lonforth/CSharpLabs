using System;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lab2");

            AdvancedPathController pathController = new AdvancedPathController();
            AdvancedMenuController menuController = new AdvancedMenuController(pathController);

            do
            {
                menuController.WriteMenuText();
            } while (menuController.PressKey());
        }
    }
}