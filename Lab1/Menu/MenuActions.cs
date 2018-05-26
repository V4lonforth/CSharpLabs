using Lab1.Input;
using Lab1.Output;

namespace Lab1.Menu
{
    public class MenuActions : IMenuActions
    {
        protected PathController pathController;
        protected IReader reader;
        protected IWriter writer;

        public MenuActions(PathController controller, IReader reader, IWriter writer)
        {
            pathController = controller;
            this.reader = reader;
            this.writer = writer;
        }

        public void ChangeState()
        {
            int index = reader.ReadNumber("Choose block: ", 1, pathController.blockStates.Length) - 1;
            BlockState state = reader.ReadState("Choose state: ");

            pathController.ChangeState(index, state);

            writer.WriteLine("Block state changed.");
        }

        public void Check()
        {
            int first = reader.ReadNumber("From: ", 1, pathController.blockStates.Length),
                last = reader.ReadNumber("To: ", first, pathController.blockStates.Length);

            int blockNumber = pathController.Check(first, last);
            if (blockNumber == 0)
                writer.WriteLine("Path is opened.");
            else
                writer.WriteLine("Result: {0} block closed.", blockNumber);
        }

        public void ViewState()
        {
            writer.WriteLine(pathController.ViewState());
        }
        
    }
}
