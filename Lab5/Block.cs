using Lab1;

namespace Lab5
{
    class Block
    {
        public int Index { get; set; }
        public int State { get; set; }

        public Block(int index, BlockState state)
        {
            Index = index + 1;
            State = (int)state;
        }
    }
}
