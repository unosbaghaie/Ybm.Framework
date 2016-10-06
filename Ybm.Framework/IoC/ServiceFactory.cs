using Ybm.Framework.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.Framework
{
    public class ServiceFactory
    {
        public static T CreateInstance<T>(string name = null) 
        {
            var instance = name == null ?
                Ybm.Framework.ContainerManager.Container.Resolve<T>() :
                Ybm.Framework.ContainerManager.Container.Resolve<T>(name);

            //Subscribe all instance's methods to their evnets
            //new SubscribeEngine().RegisterEvents(instance);
            //new ServiceEventEngine().Fire(instance);

            return instance;
        }
        public static void Release(object instance)
        {
            Ybm.Framework.ContainerManager.Container.Release(instance);
        }
        public static object CreateInstance(Type type)
        {
            var instance = Ybm.Framework.ContainerManager.Container.Resolve(type);
            return instance;
        }
    }
}
