using System.Text;
using Lab1;
using Lab1.Input;
using Lab1.Output;
using Lab2.Menu;
using Lab4;

namespace Lab4Client
{
    class ClientMenuActions : IAdvancedMenuActions
    {
        private IReader reader;
        private IWriter writer;
        private IPathControllerActions pathControllerActions;

        public ClientMenuActions(IReader r, IWriter w, IPathControllerActions actions)
        {
            reader = r;
            writer = w;
            pathControllerActions = actions;
        }

        public void ChangeState()
        {
            int index = reader.ReadNumber("Choose block: ", 1, pathControllerActions.GetPathLength()) - 1;
            BlockState state = reader.ReadState("Choose state: ");

            pathControllerActions.ChangeState(index, state);

            writer.WriteLine("Block state changed.");
        }

        public void Check()
        {
            int first = reader.ReadNumber("From: ", 1, pathControllerActions.GetPathLength()),
                last = reader.ReadNumber("To: ", first, pathControllerActions.GetPathLength());

            writer.WriteLine(pathControllerActions.Check(first, last));
        }

        public void CreateBlocks()
        {
            BlockState[] blockStates = new BlockState[reader.ReadNumber("Blocks count: ", 1, int.MaxValue)];
            for (int i = 0; i < blockStates.Length; i++)
            {
                blockStates[i] = reader.ReadState("Block " + (i + 1) + " state(R/Y/G): ");
                if (blockStates[i] == BlockState.Red && i > 0)
                    blockStates[i - 1] = BlockState.Yellow;
            }
            pathControllerActions.CreateBlocks(blockStates);
        }

        public void Load()
        {
            bool loaded = false;
            while (!loaded)
            {
                string path = reader.ReadString("Provide path:\n");
                string error = pathControllerActions.Load(path);
                if (error == null)
                    loaded = true;
                else
                    writer.WriteLine(error + " Try again.");
            }
        }

        public void Save()
        {
            bool saved = false;
            while (!saved)
            {
                string path = reader.ReadString("Provide path:\n");
                string error = pathControllerActions.Save(path);
                if (error == null)
                    saved = true;
                else
                    writer.WriteLine(error + " Try again.");
            }
        }

        public void ViewState()
        {
            BlockState[] blockStates = pathControllerActions.GetStates();
            writer.WriteLine(PathController.ViewStatesInText(blockStates));
        }
    }
}
