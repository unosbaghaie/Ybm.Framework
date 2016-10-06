using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Instagram.Framework.Reflection;

namespace Instagram.Framework
{
    internal class FastMethodInvocation : FastMethodInvocation<object>
    {
        private static ConcurrentDictionary<MethodInfo, FastMethodInvocation> methodInvokerMap = new ConcurrentDictionary<MethodInfo, FastMethodInvocation>();

        static FastMethodInvocation()
        {
        }

        private FastMethodInvocation(MethodInfo method)
            : base(method, true)
        {
        }

        public static FastMethodInvocation For(MethodInfo method)
        {
            FastMethodInvocation addValue = (FastMethodInvocation)null;
            if (!FastMethodInvocation.methodInvokerMap.TryGetValue(method, out addValue))
            {
                addValue = new FastMethodInvocation(method);
                FastMethodInvocation.methodInvokerMap.AddOrUpdate(method, addValue, (Func<MethodInfo, FastMethodInvocation, FastMethodInvocation>)((k, m) => m));
            }
            return addValue;
        }
    }
}
