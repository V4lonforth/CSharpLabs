using System;

namespace Lab1
{
    public class PathController
    {
        protected BlockState[] blockStates;
        
        public void ReadBlocksInfo()
        {
            blockStates = new BlockState[Input.ReadNumber("Blocks count: ", 1)];
            for (int i = 0; i < blockStates.Length; i++)
            {
                blockStates[i] = Input.ReadState("Block " + (i + 1) + " state(R/Y/G): ");
                if (blockStates[i] == BlockState.Red && i > 0)
                    blockStates[i - 1] = BlockState.Yellow;
            }
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

        public void Check()
        {
            int first = Input.ReadNumber("From: ", 1, blockStates.Length),
                last = Input.ReadNumber("To: ", first, blockStates.Length);
            for (int i = first; i <= last; i++)
                if (blockStates[i - 1] == BlockState.Red)
                {
                    Console.WriteLine("Result: {0} block closed.", i);
                    return;
                }
            Console.WriteLine("Path is opened.");
        }
        public void ViewState()
        {
            Console.Write(ConvertState(blockStates[0]));
            for (int i = 1; i < blockStates.Length; i++)
                Console.Write(" - {0}", ConvertState(blockStates[i]));
            Console.WriteLine();
        }
        public void ChangeState()
        {
            int index = Input.ReadNumber("Choose block: ", 1, blockStates.Length) - 1;
            BlockState state = Input.ReadState("Choose state: ");
            blockStates[index] = state;
            if (state == BlockState.Red && index > 0)
                blockStates[index - 1] = BlockState.Yellow;
        }
    }
}