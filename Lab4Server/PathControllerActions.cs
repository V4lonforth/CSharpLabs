using System;
using Lab1;
using Lab1.Input;
using Lab2;
using Lab4;

namespace Lab4Server
{
    public class PathControllerActions : IPathControllerActions
    {
        private PathController pathController;
        private Serializer serializer;
        private StringFormatChecker checker;

        public PathControllerActions()
        {
            pathController = new PathController(null);
            serializer = new Serializer();
            checker = new StringFormatChecker();
        }

        public int GetPathLength()
        {
            return pathController.blockStates.Length;
        }

        public void ChangeState(int index, BlockState state)
        {
            Console.WriteLine("State is changed");
            pathController.ChangeState(index, state);
        }

        public string Check(int first, int last)
        {
            Console.WriteLine("Check from {0} to {1}", first, last);
            Exception exception = checker.CheckNumberFormat(first.ToString(), 1, pathController.blockStates.Length);
            if (exception != null)
                return exception.Message;
            exception = checker.CheckNumberFormat(last.ToString(), first, pathController.blockStates.Length);
            if (exception != null)
                return exception.Message;
            int result = pathController.Check(first, last);
            if (result == 0)
                return "Path is opened.";
            return String.Format("Block {0} is closed", result);
        }

        public void CheckConnection()
        {
        }

        public void CreateBlocks(BlockState[] states)
        {
            Console.WriteLine("Blocks are created");
            pathController.blockStates = states;
        }

        public string Load(string path)
        {
            Exception exception = serializer.Deserialize(path, out BlockState[] states);
            if (exception is null)
            {
                Console.WriteLine("Path loaded");
                pathController.blockStates = states;
                return null;
            }
            return exception.Message;
        }

        public string Save(string path)
        {
            Exception exception = serializer.Serialize(pathController.blockStates, path);
            if (exception is null)
            {
                Console.WriteLine("Path saved");
                return null;
            }
            return exception.Message;
        }

        public BlockState[] GetStates()
        {
            Console.WriteLine("Path state is shown");
            return pathController.blockStates;
        }

        public BlockState GetState(int index)
        {
            return pathController.blockStates[index];
        }
    }
}
