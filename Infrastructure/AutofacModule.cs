using Application.Services.Infrastructure;
using Autofac;
using Infrastructure.Cache;
using Infrastructure.Persistence.Commands.Aggregates;
using System.Reflection;

namespace Infrastructure
{
    public class AutofacInfrastructureModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();

            builder
                .RegisterType<AggregatesPersistence>()
                .As<IAggregatesPersistence>()
                .SingleInstance();

            builder
               .RegisterGeneric(typeof(CacheAdapter<,>))
               .As(typeof(ICacheAdapter<,>))
               .SingleInstance();

        }
    }
}
