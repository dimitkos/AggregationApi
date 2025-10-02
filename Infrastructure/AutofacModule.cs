using Autofac;
using System.Reflection;

namespace Infrastructure
{
    public class AutofacInfrastructureModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();

        }
    }
}
