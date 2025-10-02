using Autofac;
using Gateway.Api;

namespace Gateway
{
    public class AutofacGatewayModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterGeneric(typeof(ApiClient<,>))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
