using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.Framework.Eventing
{

    [System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class SubscribeToAttribute : BusinessEventHandlerAttribute
    {
        public SubscribeToAttribute(System.Type serviceInterfaceType, string eventName) : base(serviceInterfaceType, eventName)
        {
        }
    }
}
