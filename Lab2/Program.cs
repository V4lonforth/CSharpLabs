using System;
using Lab1;
using Lab1.Input;
using Lab1.Output;
using Lab2.Menu;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lab2");
            ConsoleReader reader = new ConsoleReader();
            ConsoleWriter writer = new ConsoleWriter();
            AdvancedMenuActions menuActions = new AdvancedMenuActions(new PathController(reader), reader, writer);
            AdvancedMenuController menuController = new AdvancedMenuController(menuActions, reader, writer);

            bool finished = false;
            while (!finished)
                finished = !menuController.PressKey();
        }
    }
}