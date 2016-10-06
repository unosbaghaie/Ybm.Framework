using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.Framework.Engine
{
    public class SubscribeEngine : IEngine
    {
        public void Fire()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(q => q.FullName.Contains("Business")).ToArray();
            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            var interfaceType = typeof(Interface.IService<>);
            var types = assemblies
                .SelectMany(s => s.GetTypes())
                .Where(m => m.GetCustomAttributes(typeof(Service.ServiceAttribute), true).Any()).ToList();

            foreach (var type in types)
            {
                var methods = type.GetMethods(bindingFlags)
                    .Where(m => m.GetCustomAttributes(typeof(Ybm.Framework.Eventing.SubscribeToAttribute), true).Any()).ToList();

                foreach (var method in methods)
                {
                    var attributes = method.GetCustomAttributes().ToList();
                    foreach (var attribute in attributes)
                    {
                        var attr = (Ybm.Framework.Eventing.SubscribeToAttribute)attribute;
                        var eventName = attr.EventName;
                        var serviceInterfaceType = attr.ServiceInterfaceType;
                        var methodName = method.Name;

                        SubscribeSchema.Add(type.FullName, new SubscribeSchema()
                        {
                            SubscribeEventName = eventName,
                            SubscribeEventEnclosingType = serviceInterfaceType,
                            SubscriberMethod = method,
                            SubscriberType = type
                        });
                    }
                }
            }
        }

        public void RegisterEvents(object instance)
        {
            var type = instance.GetType();
            var subscribeSchemas = SubscribeSchema.Get(type.FullName);
            if (subscribeSchemas == null)
                return;

            foreach (var subscribeSchema in subscribeSchemas)
            {
                var subInstance = ServiceFactory.CreateInstance(subscribeSchema.SubscribeEventEnclosingType);

                EventInfo eventInfo = subscribeSchema.SubscribeEventEnclosingType.GetEvent(subscribeSchema.SubscribeEventName);
                Type typeOfEvent = eventInfo.EventHandlerType;
                Delegate handler = Delegate.CreateDelegate(typeOfEvent, instance, subscribeSchema.SubscriberMethod);

                var eventDelegate = EventHelper.GetEventHandler(subInstance, subscribeSchema.SubscribeEventName);
                if (eventDelegate == null)
                    eventInfo.AddEventHandler(subInstance, handler);
                else
                    System.Diagnostics.Debug.WriteLine(type.FullName + "Listeners: " + eventDelegate.GetInvocationList().Length);
            }
        }
    }
}
