using System.ServiceModel;

namespace TestingTools.WcfSelfHost.Tests.ServiceContract
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string Convert(int value);
    }
}