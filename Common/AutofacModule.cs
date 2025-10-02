using Autofac;

namespace Common
{
    public class AutofacCommonModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<UniqueIdGenerator>()
                .As<IIdGenerator>()
                .SingleInstance();
        }
    }
}
