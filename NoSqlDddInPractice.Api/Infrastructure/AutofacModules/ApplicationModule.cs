using Autofac;
using NoSqlDddInPractice.Data.Repositories;
using NoSqlDddInPractice.Domain.Aggregates.SnakMachineAggregate;
using NoSqlDddInPractice.Domain.ReadModels;
using NoSqlDddInPractice.Domain.ReadModels.SnackMachine;

namespace NoSqlDddInPractice.Api.Infrastructure.AutofacModules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SnackTypeReadRepository>()
                .As<ISnackTypeReadRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SnackMachineReadRepository>()
                .As<ISnackMachineReadRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SnackMachineRepository>()
                .As<ISnackMachineRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
