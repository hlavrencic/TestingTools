using System;
using System.ServiceModel;

namespace TestingTools.WcfSelfHost
{
    public class WcfClient : IDisposable
    {
        public ICommunicationObject ChannelFactory { get; set; }

        public void Dispose()
        {
            if (ChannelFactory == null)
            {
                return;
            }

            CloseCommunicationObject(ChannelFactory);
        }

        public TServiceContract CreateClient<TServiceContract>(string configName)
        {
            Dispose();

            var serviceFactory = new ChannelFactory<TServiceContract>(configName);
            ChannelFactory = serviceFactory;
            var service1 = serviceFactory.CreateChannel();
            return service1;
        }

        private static void CloseCommunicationObject(ICommunicationObject comObject)
        {
            try
            {
                if (comObject.State != CommunicationState.Faulted && comObject.State != CommunicationState.Closed)
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
