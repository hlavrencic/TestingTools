namespace TestingTools.WcfSelfHost.Tests.ServiceContract
{
    public class Service1 : IService1
    {
        public string Convert(int value)
        {
            return value.ToString();
        }
    }
}
