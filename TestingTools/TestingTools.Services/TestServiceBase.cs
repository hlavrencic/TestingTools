using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestingTools.Services
{
    [TestClass]
    public abstract class TestServiceBase<TInterface>
        where TInterface : class
    {
        private readonly Binding binding;
        private ServiceHost serviceHost;
        private ChannelFactory<TInterface> serviceFactory;

        public TInterface Service { get; set; }

        protected TestServiceBase(Binding binding)
        {
            this.binding = binding;
        }

        [TestInitialize]
        public void TestServiceBaseTestInitialize()
        {
            var serviceInstance = ServiceInstance();
            serviceHost = new ServiceHost(serviceInstance);
            var serviceBehavior = serviceHost.Description.Behaviors.Find<ServiceBehaviorAttribute>();
            serviceBehavior.InstanceContextMode = InstanceContextMode.Single;
            serviceBehavior.IncludeExceptionDetailInFaults = true;

            var typeInterface = typeof(TInterface);
            var url = "http://localhost:3000/" + typeInterface.Name;

            serviceHost.AddServiceEndpoint(typeInterface, binding, new Uri(url));
            serviceHost.Open();

            try
            {
                serviceFactory = new ChannelFactory<TInterface>(binding, new EndpointAddress(url));
                Service = serviceFactory.CreateChannel();
            }
            catch
            {
                serviceHost.Close();
            }
        }

        [TestCleanup]
        public void TestServiceBaseTestCleanup()
        {
            try
            {
                var comunicationObject = (ICommunicationObject)Service;
                CloseCommunicationObject(comunicationObject);
            }
            finally
            {
                serviceHost.Close();
            }
        }

        protected abstract TInterface ServiceInstance();

        private static void CloseCommunicationObject(ICommunicationObject comObject)
        {
            try
            {
                if (comObject.State != CommunicationState.Faulted)
                {
                    comObject.Close();
                }
            }
            catch
            {
                comObject.Abort();
            }
        }
    }
}
