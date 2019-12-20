using Backend.Repository;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Backend.Models;
using Backend.Security.Interfaces;
using Backend.Security.Implementations;
using Backend.Services;
using Backend.Repository.ExtendedRepositories;
using System.Linq;
using Backend.Shared;

namespace Backend
{
    public class DependencyInjection : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IUserService, UserService>().LifestyleTransient(),
                Component.For<IAdminService, AdminService>().LifestyleTransient(),
                Component.For<IItemService, ItemService>().LifestyleTransient(),
                Component.For<IAuthenticatedService, AuthenticatedService>().LifestyleTransient(),
                Component.For<ApplicationDbContext>().LifestyleTransient(),
                Component.For<IAuth, Auth>().LifestyleTransient(),
                Component.For<IHash, RFCHash>().LifestyleSingleton(),
                Component.For(typeof(IRepository<>), typeof(Repository<>)).LifestyleTransient(),
                Component.For<IUserRepository, UserRepository>().LifestyleTransient(),
                Component.For<IUserHistoryRepository, UserHistoryRepository>().LifestyleTransient(),
                Component.For<IItemCategoryRepository, ItemCategoryRepository>().LifestyleTransient(),
                Component.For<ICategoryRepository, CategoryRepository>().LifestyleTransient()
            );
            IUserRepository users = container.Resolve<IUserRepository>();
            if(!users.GetAll().Where(u => u.IsAdmin).Any())
            {
                users.Insert(new User
                {
                    Email = "admin@admin.com",
                    Password = container.Resolve<IHash>().Hash("AdminAdmin"),
                    IsAdmin = true
                });
            }
        }
    }
}
