using Castle.DynamicProxy;
using Ybm.Framework.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ybm.Framework.Aop
{
    public class BroadcasterInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            if (!invocation.MethodInvocationTarget.GetCustomAttributes(typeof(BroadcasterAttribute), false).Any())
                invocation.Proceed();
            else
                try
                {
                    var methodInfo = invocation.MethodInvocationTarget;
                    var attribute =  methodInfo.CustomAttributes.FirstOrDefault();
                    var type =  (Type)attribute.ConstructorArguments[0].Value;
                    var methodName = (string)attribute.ConstructorArguments[1].Value;
                    var runFirst = (bool)attribute.ConstructorArguments[2].Value;

                    dynamic instance = ServiceFactory.CreateInstance(type);

                    if (runFirst)
                    {
                        instance.GetType().GetMethod(methodName).Invoke(instance, null);
                        invocation.Proceed();
                    }
                    else
                    {
                        invocation.Proceed();
                        instance.GetType().GetMethod(methodName).Invoke(instance, null);
                    }
                }
                catch (Exception ex)
                {
                    throw new AggregateException(ex);
                }
        }
    }
}
