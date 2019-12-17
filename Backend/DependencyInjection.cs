using Backend.Repository;
using Backend.Security;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Backend.Models;

namespace Backend
{
    public class DependencyInjection : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ApplicationDbContext>(),
                Component.For<IAuth, JWTAuth>(),
                Component.For(typeof(IRepository<>), typeof(Repository<>))
            );
        }
    }
}
