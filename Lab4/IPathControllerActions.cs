using System.ServiceModel;
using Lab1;

namespace Lab4
{
    [ServiceContract]
    public interface IPathControllerActions
    {
        [OperationContract]
        int GetPathLength();

        [OperationContract]
        void CheckConnection();

        [OperationContract]
        string Check(int first, int last);

        [OperationContract]
        BlockState[] GetStates();

        [OperationContract]
        BlockState GetState(int index);

        [OperationContract]
        void ChangeState(int index, BlockState state);

        [OperationContract]
        void CreateBlocks(BlockState[] states);

        [OperationContract]
        string Save(string path);

        [OperationContract]
        string Load(string path);
    }
}
