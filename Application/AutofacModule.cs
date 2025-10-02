using Application.Behaviors;
using Autofac;
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
        }
    }
}
