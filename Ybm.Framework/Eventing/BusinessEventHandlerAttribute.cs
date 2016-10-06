using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.Framework.Eventing
{
    [System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class BusinessEventHandlerAttribute : System.Attribute
    {
        public System.Type ServiceInterfaceType
        {
            get;
            private set;
        }

        public string EventName
        {
            get;
            private set;
        }

        public bool IsAdvisor
        {
            get;
            set;
        }

        public BusinessEventHandlerAttribute(System.Type serviceInterfaceType, string eventName)
        {
            this.ServiceInterfaceType = serviceInterfaceType;
            this.EventName = eventName;
            this.IsAdvisor = true;
        }
    }
}
