using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestingTools.WcfSelfHost.Tests.ServiceContract;

namespace TestingTools.WcfSelfHost.Tests
{
    [TestClass]
    public class ServerClientTest
    {
        private SelfHostWcf host;

        private WcfClient client;

        private IService1 service;

        [TestInitialize]
        public void Init()
        {
            host = new SelfHostWcf(new ServiceProvider());
            host.Init<IService1>();

            client = new WcfClient();
            service = client.CreateClient<IService1>("IService1");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            host.Dispose();
            client.Dispose();
        }

        [TestMethod]
        public void CallTest()
        {
            var result = service.Convert(4);
            Assert.AreEqual("4", result);
        }
    }
}
