using System;
using System.Security.Permissions;
using System.ServiceModel;

namespace TestingTools.WcfSelfHost
{
    public class SelfHostWcf : IDisposable
    {
        private readonly IServiceProvider serviceProvider;

        public SelfHostWcf(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ServiceHost ServiceHost { get; private set; }

        public ServiceHost Init<TServiceContract>()
        {
            if (ServiceHost != null)
            {
                Dispose();
            }
            
            return ServiceHost = CreateHost<TServiceContract>();
        }

        public void Dispose()
        {
            if (ServiceHost == null)
            {
                return;
            }

            if (ServiceHost.State != CommunicationState.Opened)
            {
                return;
            }

            ServiceHost.Close();
        }
        
        [PrincipalPermission(SecurityAction.Demand, Role = @"BUILTIN\Administrators")]
        private ServiceHost CreateHost<TServiceContract>()
        {
            var implementedContract = typeof(TServiceContract);
            //var binding = new BasicHttpBinding();

            var serviceImplementation = serviceProvider.GetService(implementedContract);
            var host = new ServiceHost(serviceImplementation);
            var behaviorAttribute = host.Description.Behaviors.Find<ServiceBehaviorAttribute>();
            behaviorAttribute.InstanceContextMode = InstanceContextMode.Single;
            behaviorAttribute.IncludeExceptionDetailInFaults = true;
            
            //Url = string.Format("http://localhost:30000/{0}.svc", implementedContract.Name);
            
            //host.AddServiceEndpoint(implementedContract, binding, new Uri(Url));
            host.Open();

            return host;
        }
    }
}
