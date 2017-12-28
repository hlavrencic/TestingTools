using System.ServiceModel.Channels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace TestingTools.Services
{
    [TestClass]
    public abstract class TestServiceAutoInjected<TInterface, TImplementation> : TestServiceBase<TInterface>
        where TInterface : class
        where TImplementation : TInterface
    {
        private KernelBase kernel;

        protected TestServiceAutoInjected(Binding binding)
            : base(binding)
        {

        }

        [TestCleanup]
        public void TestServiceAutoMockedTestCleanup()
        {
            kernel.Dispose();
        }

        protected abstract KernelBase Kernel();

        protected override TInterface ServiceInstance()
        {
            kernel = Kernel();
            var serviceInstance = kernel.Get<TImplementation>();
            kernel.Inject(this);
            return serviceInstance;
        }
    }
}
