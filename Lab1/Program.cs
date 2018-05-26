using System;
using Lab1.Menu;
using Lab1.Input;
using Lab1.Output;

namespace Lab1
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Lab1\n");

            ConsoleReader reader = new ConsoleReader();
            ConsoleWriter writer = new ConsoleWriter();

            PathController pathController = new PathController(reader);
            pathController.ReadBlocksInfo();
            MenuActions menuActions = new MenuActions(pathController, reader, writer);
            MenuController menuController = new MenuController(menuActions, reader, writer);

            bool finished = false;
            while (!finished)
                finished = !menuController.PressKey();
        }
    }
}