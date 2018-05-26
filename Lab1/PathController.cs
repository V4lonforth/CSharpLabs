using Lab1.Input;
using System.Text;

namespace Lab1
{
    public class PathController
    {
        public BlockState[] blockStates;
        private IReader reader;

        public PathController(IReader reader)
        {
            this.reader = reader;
        }

        public void ReadBlocksInfo()
        {
            blockStates = new BlockState[reader.ReadNumber("Blocks count: ", 1, int.MaxValue)];
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
                    return i;
            return 0;
        }

        public void ChangeState(int index, BlockState state)
        {
            blockStates[index] = state;
            if (state == BlockState.Red && index > 0)
                blockStates[index - 1] = BlockState.Yellow;
        }

        public string ViewState()
        {
            string s = "";
            s += ConvertState(blockStates[0]);
            for (int i = 1; i < blockStates.Length; i++)
                s += " - " + ConvertState(blockStates[i]);
            return s;
        }

        public static string ViewStatesInText(BlockState[] blockStates)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Capacity = 1 + 4 * blockStates.Length;
            stringBuilder.Append(ConvertState(blockStates[0]));
            for (int i = 1; i < blockStates.Length; i++)
            {
                stringBuilder.Append(" - ");
                stringBuilder.Append(ConvertState(blockStates[i]));
            }
            return stringBuilder.ToString();
        }

        public static char ConvertState(BlockState state)
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