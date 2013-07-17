using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Data.Entity;
using DemoApp.Data;
using System.Data.Objects;
using System.Data.Entity.Infrastructure;
using DemoApp.Repositories;
using DemoApp.Business;
using Microsoft.Practices.ServiceLocation;

namespace DemoApp.Web.Angular
{
    public class UnityDependencyResolver : System.Web.Mvc.IDependencyResolver
    {
        private IUnityContainer container;

        public UnityDependencyResolver(IUnityContainer container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            if (!container.IsRegistered(serviceType))
            {
                if (serviceType.IsAbstract || serviceType.IsInterface)
                {
                    return null;
                }
            }
            return container.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.ResolveAll(serviceType);
        }
    }
    public class UnityScope : IDependencyScope
    {
        protected IUnityContainer Container;

        public UnityScope(IUnityContainer container)
        {
            Container = container;
        }

        public void Dispose()
        {
            IDisposable disposable = (IDisposable)Container;
            if (disposable != null)
            {
                disposable.Dispose();
            }
            Container = null;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null)
            {
                return null;
            }
            try
            {
                return Container.Resolve(serviceType);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Container.ResolveAll<object>().Where(s => s.GetType() == serviceType);
        }
    }
    public class UnityAPIDependencyResolver : UnityScope, System.Web.Http.Dependencies.IDependencyResolver
    {
        private IUnityContainer _container;

        public UnityAPIDependencyResolver(IUnityContainer container)
            : base(container)
        {
            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            return new UnityScope(_container);
        }
    }
    public class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new UnityAPIDependencyResolver(container);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<DbContext, DemoAppDataContext>(new PerCallContextOrRequestLifeTimeManager(), new InjectionConstructor());
            container.RegisterFactory<ObjectContext>(() => ((IObjectContextAdapter)container.Resolve<DbContext>()).ObjectContext, new PerCallContextOrRequestLifeTimeManager());
            
            container.RegisterType<IContentObjectRepository, ContentObjectRepository>(new PerCallContextOrRequestLifeTimeManager());

            container.RegisterType<IContentService, ContentService>(new PerCallContextOrRequestLifeTimeManager());
            container.RegisterType<IPersonService, PersonService>(new PerCallContextOrRequestLifeTimeManager());
            container.RegisterType<IBookService, BookService>(new PerCallContextOrRequestLifeTimeManager());
            return container;
        }
    }

    public class PerCallContextOrRequestLifeTimeManager : LifetimeManager, IDisposable
    {
        private string _key = string.Format("PerCallContextOrRequestLifeTimeManager_{0}", Guid.NewGuid());
        [ThreadStatic]
        private object _object;
        [ThreadStatic]
        private object _syncRoot = new object();

        public PerCallContextOrRequestLifeTimeManager()
        {
            if (HttpContext.Current != null)
                HttpContext.Current.ApplicationInstance.EndRequest += delegate { Dispose(); };
        }

        public override object GetValue()
        {
            if (HttpContext.Current != null)
                lock (HttpContext.Current.Items)
                    return HttpContext.Current.Items[this._key.ToString()];
            else
                lock (_syncRoot)
                    return _object;
        }

        public override void SetValue(object aNewValue)
        {
            if (HttpContext.Current != null)
            {
                lock (HttpContext.Current.Items)
                {
                    HttpContext.Current.Items[this._key.ToString()] = aNewValue;
                }
            }
            else
                lock (_syncRoot)
                    _object = aNewValue;
        }

        public override void RemoveValue()
        {
            object value = GetValue();
            IDisposable disposableValue = value as IDisposable;
            if (disposableValue != null)
                disposableValue.Dispose();

            if (HttpContext.Current != null)
                lock (HttpContext.Current.Items)
                    HttpContext.Current.Items.Remove(this._key.ToString());
            else
                lock (_syncRoot)
                    _object = null;
        }

        public void Dispose()
        {
            RemoveValue();
        }
    }

    public class FactoryLifetimeManager<T> : LifetimeManager
    {
        private Func<T> _factoryMethod;
        private LifetimeManager _baseManager;

        public FactoryLifetimeManager(Func<T> factoryMethod, LifetimeManager baseManager)
        {
            _factoryMethod = factoryMethod;
            _baseManager = baseManager;
        }

        public override object GetValue()
        {
            var obj = _baseManager.GetValue();
            if (obj == null)
            {
                obj = _factoryMethod();
                SetValue(obj);
            }
            return obj;
        }

        public override void RemoveValue()
        {
            _baseManager.RemoveValue();
        }

        public override void SetValue(object newValue)
        {
            _baseManager.SetValue(newValue);
        }
    }

    public static class UnityFactoryMethodExtensions
    {

        public static IUnityContainer RegisterFactory<T>(this IUnityContainer container, Func<T> factoryMethod)
        {
            return container.RegisterFactory<T>(factoryMethod, new TransientLifetimeManager());
        }

        public static IUnityContainer RegisterFactory<T>(this IUnityContainer container, Func<T> factoryMethod, LifetimeManager lifetimeManager)
        {
            return container.RegisterType<T>(new FactoryLifetimeManager<T>(factoryMethod, lifetimeManager));
        }

        public static IUnityContainer RegisterFactory<T>(this IUnityContainer container, Func<T> factoryMethod, string name)
        {
            return container.RegisterFactory<T>(factoryMethod, name, new TransientLifetimeManager());
        }

        public static IUnityContainer RegisterFactory<T>(this IUnityContainer container, Func<T> factoryMethod, string name, LifetimeManager lifetimeManager)
        {
            return container.RegisterType<T>(name, new FactoryLifetimeManager<T>(factoryMethod, lifetimeManager));
        }
    }
}