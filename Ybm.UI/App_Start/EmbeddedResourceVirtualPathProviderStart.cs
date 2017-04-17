using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Ybm.UI.App_Start
{
    public static class EmbeddedResourceVirtualPathProviderStart
    {
        public static void Start()
        {
            //By default, we scan all non system assemblies for embedded resources
            var assemblies = System.Web.Compilation.BuildManager.GetReferencedAssemblies()
                .Cast<Assembly>()
                .Where(a => a.GetName().Name.StartsWith("System") == false);

            var vpp = new Framework.EmbeddedResourceVirtualPathProvider.Vpp(assemblies.ToArray());

            System.Web.Hosting.HostingEnvironment.RegisterVirtualPathProvider(vpp);
        }
    }
}