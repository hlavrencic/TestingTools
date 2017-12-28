using System.ServiceModel;
using System.ServiceModel.Channels;

namespace TestingTools.Services.Providers
{
    public static class BindingProvider
    {
        public static Binding CreateStreamedBinding()
        {
            return new BasicHttpBinding
            {
                MaxReceivedMessageSize = 2147483647,
                MaxBufferPoolSize = 2147483647,
                MaxBufferSize = 2147483647,
                TransferMode = TransferMode.Streamed
            };
        }
    }
}
