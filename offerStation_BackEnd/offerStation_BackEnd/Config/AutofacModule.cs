using Autofac;
using offerStation.Core.Interfaces.Repositories;
using offerStation.Core.Interfaces.Services;
using offerStation.EF;
using offerStation.EF.Data;
using offerStation.EF.Repositories;

namespace offerStation.API.Config
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(ApplicationDbContext)).InstancePerLifetimeScope();  
            builder.RegisterType(typeof(UnitOfWork)).InstancePerLifetimeScope();  
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(AccountService).Assembly).InstancePerLifetimeScope();
        }
    }
}
