using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace Ybm.Framework.Reflection
{
    internal class FastMethodInvocation<TResult>
    {
        private static ConcurrentDictionary<MethodInfo, FastMethodInvocation<TResult>> methodInvokerMap = new ConcurrentDictionary<MethodInfo, FastMethodInvocation<TResult>>();
        private Func<object, object[], TResult> invoker;
        private MethodInfo method;

        static FastMethodInvocation()
        {
        }

        private FastMethodInvocation(MethodInfo method)
            : this(method, false)
        {
        }

        protected FastMethodInvocation(MethodInfo method, bool doConversion)
        {
            this.method = method;
            ParameterExpression parameterExpression1 = Expression.Parameter(typeof(object));
            ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object[]));
            UnaryExpression unaryExpression = Expression.Convert((Expression)parameterExpression1, method.DeclaringType);
            ParameterInfo[] parameters = method.GetParameters();
            List<Expression> list1 = new List<Expression>();
            List<ParameterExpression> list2 = new List<ParameterExpression>();
            List<Expression> list3 = new List<Expression>();
            List<Expression> list4 = new List<Expression>();
            List<Expression> list5 = new List<Expression>();
            for (int index1 = 0; index1 < parameters.Length; ++index1)
            {
                int index2 = index1;
                ParameterInfo parameterInfo = parameters[index2];
                Type type = parameterInfo.ParameterType.IsByRef ? parameterInfo.ParameterType.GetElementType() : parameterInfo.ParameterType;
                IndexExpression indexExpression = Expression.ArrayAccess((Expression)parameterExpression2, new Expression[1]
        {
          (Expression) Expression.Constant((object) index2)
        });
                Expression right = type == typeof(object) ? (Expression)indexExpression : (Expression)Expression.Convert((Expression)indexExpression, type);
                if (parameterInfo.ParameterType.IsByRef)
                {
                    ParameterExpression parameterExpression3 = Expression.Variable(parameterInfo.ParameterType.GetElementType(), parameterInfo.Name);
                    list2.Add(parameterExpression3);
                    list1.Add((Expression)parameterExpression3);
                    list3.Add((Expression)Expression.Assign((Expression)parameterExpression3, right));
                    list5.Add((Expression)Expression.Assign((Expression)indexExpression, parameterExpression3.Type.IsValueType ? (Expression)Expression.Convert((Expression)parameterExpression3, typeof(object)) : (Expression)parameterExpression3));
                }
                else
                    list1.Add(right);
            }
            if (method.IsStatic)
                list4.Add((Expression)Expression.Call(method, (IEnumerable<Expression>)list1));
            else
                list4.Add((Expression)Expression.Call((Expression)unaryExpression, method, (IEnumerable<Expression>)list1));
            ParameterExpression parameterExpression4 = Expression.Variable(typeof(TResult), "result");
            list2.Add(parameterExpression4);
            if (method.ReturnType == typeof(void))
            {
                list4.Add((Expression)Expression.Assign((Expression)parameterExpression4, (Expression)Expression.Constant((object)default(TResult), typeof(TResult))));
            }
            else
            {
                Expression expression = Enumerable.First<Expression>((IEnumerable<Expression>)list4);
                if (doConversion && expression.Type.IsValueType)
                    expression = (Expression)Expression.Convert(expression, typeof(TResult));
                list4[0] = (Expression)Expression.Assign((Expression)parameterExpression4, expression);
            }
            list5.Add((Expression)parameterExpression4);
            this.invoker = Expression.Lambda<Func<object, object[], TResult>>((Expression)Expression.Block(typeof(TResult), (IEnumerable<ParameterExpression>)list2, Enumerable.Concat<Expression>(Enumerable.Concat<Expression>((IEnumerable<Expression>)list3, (IEnumerable<Expression>)list4), (IEnumerable<Expression>)list5)), new ParameterExpression[2]
      {
        parameterExpression1,
        parameterExpression2
      }).Compile();
        }

        public TResult Invoke(object target, object[] parameters)
        {
            try
            {
                return this.invoker(target, parameters);
            }
            catch (Exception ex)
            {
                StringBuilder stringBuilder = new StringBuilder("Invocation failed: Type=").Append((object)this.method.DeclaringType);
                stringBuilder.Append(", Method=").Append((object)this.method).Append("; Target=");
                if (target != null)
                    stringBuilder.Append(target).Append(", Type=").Append((object)target.GetType());
                else
                    stringBuilder.Append("null");
                int num = -1;
                foreach (object obj in parameters)
                {
                    ++num;
                    stringBuilder.Append("; Parameter[").Append(num).Append("]=");
                    if (obj != null)
                        stringBuilder.Append(obj).Append(", Type=").Append((object)obj.GetType());
                    else
                        stringBuilder.Append("null");
                }
                throw new TargetInvocationException(((object)stringBuilder).ToString(), ex);
            }
        }

        public static FastMethodInvocation<TResult> For(MethodInfo method)
        {
            FastMethodInvocation<TResult> addValue;
            if (!FastMethodInvocation<TResult>.methodInvokerMap.TryGetValue(method, out addValue))
            {
                addValue = new FastMethodInvocation<TResult>(method);
                FastMethodInvocation<TResult>.methodInvokerMap.AddOrUpdate(method, addValue, (Func<MethodInfo, FastMethodInvocation<TResult>, FastMethodInvocation<TResult>>)((k, m) => m));
            }
            return addValue;
        }
    }
}
