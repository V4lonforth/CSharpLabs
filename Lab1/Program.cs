using System;

namespace Lab1
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Lab1\n");

            MenuController menuController = new MenuController();

            do
            {
                menuController.WriteMenuText();
            } while (menuController.PressKey());
        }
    }
}