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
    internal class FastPropertySetter<TProperty>
    {
        private static ConcurrentDictionary<PropertyInfo, FastPropertySetter<TProperty>> propertySetterMap = new ConcurrentDictionary<PropertyInfo, FastPropertySetter<TProperty>>();
        private Action<object, TProperty> setter;

        static FastPropertySetter()
        {
        }

        private FastPropertySetter(PropertyInfo property)
            : this(property, false)
        {
        }

        protected FastPropertySetter(PropertyInfo property, bool doConversion)
        {
            ParameterExpression parameterExpression1 = Expression.Parameter(typeof(object));
            ParameterExpression parameterExpression2 = Expression.Parameter(typeof(TProperty));
            UnaryExpression unaryExpression1 = Expression.Convert((Expression)parameterExpression1, property.DeclaringType);
            UnaryExpression unaryExpression2 = Expression.Convert((Expression)parameterExpression2, property.PropertyType);
            this.setter = Expression.Lambda<Action<object, TProperty>>((Expression)Expression.Assign((Expression)Expression.Property((Expression)unaryExpression1, property), (Expression)unaryExpression2), new ParameterExpression[2]
      {
        parameterExpression1,
        parameterExpression2
      }).Compile();
        }

        public void Invoke(object target, TProperty value)
        {
            this.setter(target, value);
        }

        public static FastPropertySetter<TProperty> For(PropertyInfo property)
        {
            FastPropertySetter<TProperty> addValue = (FastPropertySetter<TProperty>)null;
            if (!FastPropertySetter<TProperty>.propertySetterMap.TryGetValue(property, out addValue))
            {
                addValue = new FastPropertySetter<TProperty>(property);
                FastPropertySetter<TProperty>.propertySetterMap.AddOrUpdate(property, addValue, (Func<PropertyInfo, FastPropertySetter<TProperty>, FastPropertySetter<TProperty>>)((k, s) => s));
            }
            return addValue;
        }
    }
}
