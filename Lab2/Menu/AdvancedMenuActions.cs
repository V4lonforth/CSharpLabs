using System;
using Lab1;
using Lab1.Input;
using Lab1.Output;
using Lab1.Menu;

namespace Lab2.Menu
{
    public class AdvancedMenuActions : MenuActions, IAdvancedMenuActions
    {
        private Serializer serializer;

        public AdvancedMenuActions(PathController pathController, IReader reader, IWriter writer) : base(pathController, reader, writer)
        {
            serializer = new Serializer();
        }
        
        public void CreateBlocks()
        {
            pathController.ReadBlocksInfo();
        }
        
        public void Save()
        {
            bool saved = false;
            while (!saved)
            {
                string path = reader.ReadString("Provide path:\n");
                Exception exception = serializer.Serialize(pathController.blockStates, path);
                if (exception == null)
                    saved = true;
                else
                    writer.WriteLine(exception.Message + " Try again.");
            }
        }

        public void Load()
        {
            bool loaded = false;
            while (!loaded)
            {
                BlockState[] buf;
                string path = reader.ReadString("Provide path:\n");
                Exception exception = serializer.Deserialize(path, out buf);

                if (exception == null)
                {
                    loaded = true;
                    pathController.blockStates = buf;
                }
                else
                    writer.WriteLine(exception.Message + " Try again.");
            }
        }
    }
}
