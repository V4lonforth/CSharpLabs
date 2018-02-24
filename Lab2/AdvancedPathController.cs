using Lab1;

namespace Lab2
{
    class AdvancedPathController : PathController
    {
        private Serializer serializer;

        public AdvancedPathController() : base()
        {
            serializer = new Serializer();
        }

        public void Save()
        {
            serializer.Serialize(blockStates);
        }
        public void Load()
        {
            BlockState[] buf = serializer.Deserialize();
            if (buf != null)
                blockStates = buf;
        }
    }
}