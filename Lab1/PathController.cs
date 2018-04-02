using System;
using Lab1.Input;

namespace Lab1
{
    public class PathController
    {
        public BlockState[] blockStates;
        private Reader reader;

        public PathController()
        {
            reader = new Reader();
        }

        public void ReadBlocksInfo()
        {
            blockStates = new BlockState[reader.ReadNumber("Blocks count: ", 1)];
            for (int i = 0; i < blockStates.Length; i++)
            {
                blockStates[i] = reader.ReadState("Block " + (i + 1) + " state(R/Y/G): ");
                if (blockStates[i] == BlockState.Red && i > 0)
                    blockStates[i - 1] = BlockState.Yellow;
            }
        }
        
        public int Check(int first, int last)
        {
            for (int i = first; i <= last; i++)
                if (blockStates[i - 1] == BlockState.Red)
                {
                    Console.WriteLine();
                    return i;
                }
            return 0;
        }

        public void ChangeState(int index, BlockState state)
        {
            blockStates[index] = state;
            if (state == BlockState.Red && index > 0)
                blockStates[index - 1] = BlockState.Yellow;
        }
    }
}