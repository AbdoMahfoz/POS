using Backend.Repository;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Backend.Models;
using Backend.Security.Interfaces;
using Backend.Security.Implementations;
using Backend.Services;
using Backend.Repository.ExtendedRepositories;

namespace Backend
{
    public class DependencyInjection : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ITestService, TestService>().LifestyleTransient(),
                Component.For<ApplicationDbContext>().LifestyleTransient(),
                Component.For<IAuth, Auth>().LifestyleTransient(),
                Component.For<IHash, Hash>().LifestyleSingleton(),
                Component.For(typeof(IRepository<>), typeof(Repository<>)).LifestyleTransient(),
                Component.For<IUserRepository, UserRepository>().LifestyleTransient()
            );
        }
    }
}
