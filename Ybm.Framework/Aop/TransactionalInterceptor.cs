using Castle.DynamicProxy;
using Ybm.Framework.Attribute;
using System;
using System.Linq;
using System.Transactions;

namespace Ybm.Framework.Aop
{
    public class TransactionalInterceptor : IInterceptor
    {
        //public TransactionScopeOption ScopeOption
        //{
        //    get
        //    {
        //        return ((TransactionalServiceBehavior)base.Behavior).ScopeOption;
        //    }
        //    set
        //    {
        //        ((TransactionalServiceBehavior)base.Behavior).ScopeOption = value;
        //    }
        //}

        //public IsolationLevel IsolationLevel
        //{
        //    get
        //    {
        //        return ((TransactionalServiceBehavior)base.Behavior).Options.IsolationLevel;
        //    }
        //    set
        //    {
        //        TransactionOptions options = ((TransactionalServiceBehavior)base.Behavior).Options;
        //        TransactionOptions options2 = default(TransactionOptions);
        //        options2.Timeout = options.Timeout;
        //        options2.IsolationLevel = value;
        //        ((TransactionalServiceBehavior)base.Behavior).Options = options2;
        //    }
        //}

        //public System.TimeSpan Timeout
        //{
        //    get
        //    {
        //        return ((TransactionalServiceBehavior)base.Behavior).Options.Timeout;
        //    }
        //    set
        //    {
        //        TransactionOptions options = ((TransactionalServiceBehavior)base.Behavior).Options;
        //        TransactionOptions options2 = default(TransactionOptions);
        //        options2.Timeout = value;
        //        options2.IsolationLevel = options.IsolationLevel;
        //        ((TransactionalServiceBehavior)base.Behavior).Options = options2;
        //    }
        //}

        public void Intercept(IInvocation invocation)
        {
            if (!invocation.MethodInvocationTarget.GetCustomAttributes(typeof(TransactionalAttribute), false).Any())
                invocation.Proceed();
            else
                try
                {
                    using (var scope = new TransactionScope())
                    {
                        invocation.Proceed();
                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    throw new AggregateException(ex);
                }
        }
    }
}
