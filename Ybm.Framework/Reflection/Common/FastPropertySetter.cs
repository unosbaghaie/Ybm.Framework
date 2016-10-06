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
    internal class FastPropertySetter : FastPropertySetter<object>
    {
        private static ConcurrentDictionary<PropertyInfo, FastPropertySetter> propertySetterMap = new ConcurrentDictionary<PropertyInfo, FastPropertySetter>();

        static FastPropertySetter()
        {
        }

        private FastPropertySetter(PropertyInfo property)
            : base(property, true)
        {
        }

        public static FastPropertySetter For(PropertyInfo property)
        {
            FastPropertySetter addValue = (FastPropertySetter)null;
            if (!FastPropertySetter.propertySetterMap.TryGetValue(property, out addValue))
            {
                addValue = new FastPropertySetter(property);
                FastPropertySetter.propertySetterMap.AddOrUpdate(property, addValue, (Func<PropertyInfo, FastPropertySetter, FastPropertySetter>)((k, s) => s));
            }
            return addValue;
        }
    }
}
