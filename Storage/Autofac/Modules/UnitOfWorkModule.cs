using Autofac;
using Common.Repositories;
using Module = Autofac.Module;


namespace Storage.Autofac.Modules
{
    public class UnitOfWorkModule
        : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder
                .RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerDependency();
        }
    }
}
