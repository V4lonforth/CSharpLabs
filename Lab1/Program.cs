using System;
using Lab1.Menu;

namespace Lab1
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Lab1\n");

            PathController pathController = new PathController();
            pathController.ReadBlocksInfo();
            MenuActions menuActions = new MenuActions(pathController);
            MenuController menuController = new MenuController(menuActions);

            do
            {
                menuController.WriteMenuText();
            } while (menuController.PressKey());
        }
    }
}