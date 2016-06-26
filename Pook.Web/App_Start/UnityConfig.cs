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
            container.RegisterType<IGenericRepository<Note>, GenericRepository<Note>>();
            container.RegisterType<IGenericRepository<Category>, GenericRepository<Category>>();
            container.RegisterType<IGenericRepository<Firm>, GenericRepository<Firm>>();
            container.RegisterType<IGenericRepository<Editor>, GenericRepository<Editor>>();

            container.RegisterType<IUserStore<User>, UserStore<User>>();
            container.RegisterType<DbContext, PookDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<UserManager<User>>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<User>, UserStore<User>>(new HierarchicalLifetimeManager());
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}