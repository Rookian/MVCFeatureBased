using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using FluentValidation;
using MediatR;
using Microsoft.Owin;
using MVCFeatureBased.Web;
using MVCFeatureBased.Web.Infrastructure;
using MVCFeatureBased.Web.Infrastructure.MVC;
using MVCFeatureBased.Web.Infrastructure.SimpleInjector;
using MVCFeatureBased.Web.Infrastructure.SimpleInjector.Conventions;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

[assembly: WebActivator.PostApplicationStartMethod(typeof(SimpleInjectorInitializer), "Initialize")]

namespace MVCFeatureBased.Web
{
    public static class SimpleInjectorInitializer
    {
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainer(Container container)
        {
         
            // MediatR
            var assemblies = new[] { typeof(SimpleInjectorInitializer).Assembly };
            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Register(typeof(IAsyncRequestHandler<,>), assemblies);
            container.RegisterCollection(typeof(INotificationHandler<>), assemblies);
            container.RegisterCollection(typeof(IAsyncNotificationHandler<>), assemblies);
            container.RegisterSingleton(new SingleInstanceFactory(container.GetInstance));
            container.RegisterSingleton(new MultiInstanceFactory(container.GetAllInstances));

            // OWIN
            container.Register(() => container.IsVerifying ?
                new OwinContext(new Dictionary<string, object>()).Authentication :
                HttpContext.Current.Request.GetOwinContext().Authentication, Lifestyle.Scoped);

            // MVC
            container.Register<HttpResponseBase>(() => new HttpResponseWrapper(HttpContext.Current.Response));
            container.Register<IUrlHelper, Infrastructure.MVC.UrlHelper>(Lifestyle.Singleton);

            // FluentValidation
            container.RegisterCollection(typeof(IValidator<>), assemblies);

            // Clock
            container.Register<IClock, Clock>(Lifestyle.Singleton);

        }

        private class Undefined { }
    }
}