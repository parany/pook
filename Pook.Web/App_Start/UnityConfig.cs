using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using Pook.Data;
using Pook.Data.Entities;
using Pook.Data.Repositories.Concrete;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Concrete;
using Pook.Service.Coordinator.Interface;
using Pook.Web.Controllers;
using Unity.Mvc5;

namespace Pook.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<AccountController>(new InjectionConstructor());

            container.RegisterType<IGenericRepository<Book>, GenericRepository<Book>>();
            container.RegisterType<IGenericRepository<Responsability>, GenericRepository<Responsability>>();
            container.RegisterType<IGenericRepository<ResponsabilityType>, GenericRepository<ResponsabilityType>>();
            container.RegisterType<IGenericRepository<Note>, GenericRepository<Note>>();
            container.RegisterType<IGenericRepository<Category>, GenericRepository<Category>>();
            container.RegisterType<IGenericRepository<Firm>, GenericRepository<Firm>>();
            container.RegisterType<IGenericRepository<Editor>, GenericRepository<Editor>>();
            container.RegisterType<IGenericRepository<Progression>, GenericRepository<Progression>>();
            container.RegisterType<IGenericRepository<Status>, GenericRepository<Status>>();
            container.RegisterType<IGenericRepository<Author>, GenericRepository<Author>>();
            container.RegisterType<IUserRepository, UserRepository>();

            container.RegisterType<IUserStore<User>, UserStore<User>>();
            container.RegisterType<DbContext, PookDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<UserManager<User>>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<User>, UserStore<User>>(new HierarchicalLifetimeManager());
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));
            
            container.RegisterType<IAuthorService, AuthorService>();
            container.RegisterType<IBookService, BookService>();
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<IEditorService, EditorService>();
            container.RegisterType<IFirmService, FirmService>();
            container.RegisterType<INoteService, NoteService>();
            container.RegisterType<IProgressionService, ProgressionService>();
            container.RegisterType<IResponsabilityTypeService, ResponsabilityTypeService>();
            container.RegisterType<IStatusService, StatusService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IResponsabilityService, ResponsabilityService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}