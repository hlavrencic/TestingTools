using System;

namespace TestingTools.WcfSelfHost.Tests.ServiceContract
{
    internal class ServiceProvider : IServiceProvider
    {
        #region Implementation of IServiceProvider

        public object GetService(Type serviceType)
        {
            return new Service1();
        }

        #endregion
    }
}