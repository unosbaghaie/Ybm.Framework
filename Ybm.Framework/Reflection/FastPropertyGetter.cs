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
    internal class FastPropertyGetter<TProperty>
    {
        private static ConcurrentDictionary<PropertyInfo, FastPropertyGetter<TProperty>> propertyGetterMap = new ConcurrentDictionary<PropertyInfo, FastPropertyGetter<TProperty>>();
        private Func<object, TProperty> getter;

        static FastPropertyGetter()
        {
        }

        private FastPropertyGetter(PropertyInfo property)
            : this(property, false)
        {
        }

        protected FastPropertyGetter(PropertyInfo property, bool doConversion)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(object));
            Expression expression = (Expression)Expression.Property((Expression)Expression.Convert((Expression)parameterExpression, property.DeclaringType), property);
            if (doConversion && expression.Type.IsValueType)
                expression = (Expression)Expression.Convert(expression, typeof(object));
            this.getter = Expression.Lambda<Func<object, TProperty>>(expression, new ParameterExpression[1]
      {
        parameterExpression
      }).Compile();
        }

        public TProperty Invoke(object target)
        {
            return this.getter(target);
        }

        public static FastPropertyGetter<TProperty> For(PropertyInfo property)
        {
            FastPropertyGetter<TProperty> addValue = (FastPropertyGetter<TProperty>)null;
            if (!FastPropertyGetter<TProperty>.propertyGetterMap.TryGetValue(property, out addValue))
            {
                addValue = new FastPropertyGetter<TProperty>(property);
                FastPropertyGetter<TProperty>.propertyGetterMap.AddOrUpdate(property, addValue, (Func<PropertyInfo, FastPropertyGetter<TProperty>, FastPropertyGetter<TProperty>>)((k, g) => g));
            }
            return addValue;
        }
    }
}
