using System.ServiceModel;

namespace TestingTools.Services
{
    public abstract class TestServiceBuffered<TInterface> : TestServiceBase<TInterface>
        where TInterface : class
    {
        protected TestServiceBuffered()
            : base(new BasicHttpBinding())
        {
        }
    }
}
