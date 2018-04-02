using System;
using Lab1.Input;

namespace Lab1.Menu
{
    public class MenuActions : IMenuActions
    {
        protected PathController pathController;
        protected Reader reader;

        public MenuActions(PathController controller)
        {
            pathController = controller;
            reader = new Reader();
        }

        public void ChangeState()
        {
            int index = reader.ReadNumber("Choose block: ", 1, pathController.blockStates.Length) - 1;
            BlockState state = reader.ReadState("Choose state: ");

            pathController.ChangeState(index, state);

            Console.WriteLine("Block state changed.");
        }

        public void Check()
        {
            int first = reader.ReadNumber("From: ", 1, pathController.blockStates.Length),
                last = reader.ReadNumber("To: ", first, pathController.blockStates.Length);

            int blockNumber = pathController.Check(first, last);
            if (blockNumber == 0)
                Console.WriteLine("Path is opened.");
            else
                Console.WriteLine("Result: {0} block closed.", blockNumber);
        }

        public void ViewState()
        {
            Console.Write(ConvertState(pathController.blockStates[0]));
            for (int i = 1; i < pathController.blockStates.Length; i++)
                Console.Write(" - {0}", ConvertState(pathController.blockStates[i]));
            Console.WriteLine();
        }
        
        private char ConvertState(BlockState state)
        {
            switch (state)
            {
                case BlockState.Yellow:
                    return 'Y';
                case BlockState.Green:
                    return 'G';
                case BlockState.Red:
                    return 'R';
            }
            return '-';
        }
    }
}
