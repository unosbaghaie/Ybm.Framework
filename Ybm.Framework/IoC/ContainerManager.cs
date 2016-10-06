using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Ybm.Framework.Attribute;

namespace Ybm.Framework
{
    public static class ContainerManager
    {

        private static IWindsorContainer container = null;
        public static IWindsorContainer Container
        {
            get
            {
                if (container == null)
                {
                    // run installers, set _container = new container
                    BootstrapContainer();
                }
                return container;
            }
        }

        public static void BootstrapContainer()
        {
            container = new WindsorContainer().Install(FromAssembly.This());

            container.Kernel.ComponentRegistered += Kernel_ComponentRegistered;

            var appDomain = System.AppDomain.CurrentDomain;
            var basePath = appDomain.RelativeSearchPath ?? appDomain.BaseDirectory;
            var path = basePath + "\\";
            container.Install(FromAssembly.InDirectory(new AssemblyFilter(path, "Ybm.Business.dll")));

        }

        public static void Dispose()
        {
            //container.Dispose();
        }

        static void Kernel_ComponentRegistered(string key, Castle.MicroKernel.IHandler handler)
        {
            //Intercept all methods of classes those have at least one method that has Transactional attribute.
            foreach (var method in handler.ComponentModel.Implementation.GetMethods())
            {
                if (Reflection.ReflectionHelper.HasMethodTheAttribute<TransactionalAttribute>(method))
                {
                    handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(Aop.TransactionalInterceptor)));
                    return;
                }


                if (Reflection.ReflectionHelper.HasMethodTheAttribute<BroadcasterAttribute>(method))
                {
                    handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(Aop.BroadcasterInterceptor)));
                    return;
                }
            }
        }
    }
}
