using System;
using Lab1;
using Lab2.Menu;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lab2");
            
            AdvancedMenuActions advancedMenuActions = new AdvancedMenuActions(new PathController());
            AdvancedMenuController menuController = new AdvancedMenuController(advancedMenuActions);

            do
            {
                menuController.WriteMenuText();
            } while (menuController.PressKey());
        }
    }
}