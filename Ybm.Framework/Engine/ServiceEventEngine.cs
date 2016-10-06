using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.Framework.Engine
{
    public class ServiceEventEngine 
    {
        public void Fire(object instance)
        {


            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(q => q.FullName.Contains("Business")).ToArray();
            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            var interfaceType = typeof(Interface.IService<>);
            var types = assemblies
                .SelectMany(s => s.GetTypes())
                .Where(m => m.GetCustomAttributes(typeof(Service.ServiceAttribute), true).Any()).ToList();

            var basType = assemblies
               .Select(s => s.GetType())
               .Where(m => m.IsAssignableFrom(typeof(Ybm.Framework.Service.Service<>))).FirstOrDefault();



            var overriddenMethods2 = instance.GetType().GetMethods().Where(q => q.Name.StartsWith("On") && q.DeclaringType != instance.GetType());
            foreach (var method in overriddenMethods2)
            {

                //var eventInfo = basType.GetEvent(method.Name.Replace("On", ""));




            }

            foreach (var type in types)
            {
                var overriddenMethods = type.GetMethods().Where(q => q.Name.StartsWith("On") && q.DeclaringType != type);
                foreach (var method in overriddenMethods)
                {   



                }
            }






            //var type = assemblies
            //   .Select(s => s.GetType())
            //   .Where(m => m.IsAssignableFrom(typeof(Ybm.Framework.Service.Service<>))).FirstOrDefault();



        }
    }
}