using System;
using System.ServiceModel;
using Lab1.Input;
using Lab1.Output;
using Lab2.Menu;
using Lab4;

namespace Lab4Client
{
    class Client
    {
        private static void Pause(IReader reader)
        {
            reader.ReadString("Type enter to exit.");
        }

        static void Main(string[] args)
        {
            IPathControllerActions actions = null;

            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            writer.WriteLine("Lab4");

            try
            {
                ChannelFactory<IPathControllerActions> factory = new ChannelFactory<IPathControllerActions>(new NetNamedPipeBinding(), new EndpointAddress("net.pipe://localhost/pathController"));
                actions = factory.CreateChannel();
            }
            catch (Exception e)
            {
                writer.WriteLine(e.Message);
                Pause(reader);
                return;
            }

            ClientMenuActions clientMenuActions = new ClientMenuActions(reader, writer, actions);
            AdvancedMenuController menuController = new AdvancedMenuController(clientMenuActions, reader, writer);

            bool finished = false;
            while (!finished)
            {
                try
                {
                    finished = !menuController.PressKey();
                }
                catch (Exception e)
                {
                    writer.WriteLine(e.Message);
                    finished = true;
                }
            }
            Pause(reader);
        }
    }
}
