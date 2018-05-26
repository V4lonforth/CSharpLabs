using System;
using System.Collections.ObjectModel;
using Lab1;
using Lab4;

namespace Lab5
{
    class PathControllerViewModel
    {
        private IPathControllerActions pathControllerActions;

        public ObservableCollection<Block> Blocks { get; set; }

        public PathControllerViewModel(IPathControllerActions actions)
        {
            pathControllerActions = actions;
            Blocks = new ObservableCollection<Block>();
            Initialize();
        }
        private string Initialize()
        {
            BlockState[] states = null;
            try
            {
                states = pathControllerActions.GetStates();
            }
            catch (Exception e)
            {
                return e.Message;
            }
            Blocks.Clear();
            for (int i = 0; i < states.Length; i++)
                Blocks.Add(new Block(i, states[i]));
            return null;
        }

        public string ChangeState(int index, BlockState state)
        {
            index--;
            try
            {
                pathControllerActions.ChangeState(index, state);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            if (index > 0)
            {
                index--;
                try
                {
                    state = pathControllerActions.GetState(index);
                    pathControllerActions.ChangeState(index, state);
                }
                catch (Exception e)
                {
                    return e.Message;
                }
                Blocks[index] = new Block(index, state);
            }
            return null;
        }

        public string Load(string path)
        {
            string error = null;
            try
            {
                error = pathControllerActions.Load(path);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            if (error is null)
            {
                Initialize();
                return "Loaded.";
            }
            else
                return error;
        }
        public string Save(string path)
        {
            string error = null;
            try
            {
                error = pathControllerActions.Save(path);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            if (error is null)
                return "Saved.";
            else
                return error;
        }

        public string Check(int first, int last)
        {
            string result = null;
            try
            {
                result = pathControllerActions.Check(first, last);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return result;
        }
    }
}
