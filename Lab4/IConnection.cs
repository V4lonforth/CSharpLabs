using System.ServiceModel;

namespace Lab4
{
    [ServiceContract]
    public interface IConnection
    {
        [OperationContract]
        int Connect();
    }
}
