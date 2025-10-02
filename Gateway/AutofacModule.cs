using Autofac;
using Gateway.Api;
using Gateway.Decorators;

namespace Gateway
{
    public class AutofacGatewayModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterGeneric(typeof(ApiClient<>))
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterGenericDecorator(typeof(StatisticsApiDecorator<>), typeof(IApiClient<>));
        }
    }
}
