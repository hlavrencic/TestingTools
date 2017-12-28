using TestingTools.Services.Providers;

namespace TestingTools.Services
{
    public abstract class TestServiceStreamed<TInterface> : TestServiceBase<TInterface>
        where TInterface : class
    {
        protected TestServiceStreamed()
            : base(BindingProvider.CreateStreamedBinding())
        {
        }
    }
}
