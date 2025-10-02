using Application.Behaviors;
using Application.Services;
using Autofac;
using Gateway.Services;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using System.Reflection;

namespace Application
{
    public class AutofacApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();

            var configuration = MediatRConfigurationBuilder
                .Create(thisAssembly)
                .WithAllOpenGenericHandlerTypesRegistered()
                .WithRegistrationScope(RegistrationScope.Scoped)
                .WithCustomPipelineBehavior(typeof(ValidationBehavior<,>))
                .Build();

            builder.RegisterMediatR(configuration);

            builder
                .RegisterType<AggregatorDataService>()
                .As<IAggregatorDataService>()
                .SingleInstance();

            builder
                .RegisterType<StatisticsService>()
                .As<IStatisticsService>()
                .SingleInstance();
        }
    }
}
