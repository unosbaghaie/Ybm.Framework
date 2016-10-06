using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Framework.Reflection
{
    internal class FastPropertyGetter : FastPropertyGetter<object>
    {
        private static ConcurrentDictionary<PropertyInfo, FastPropertyGetter> propertyGetterMap = new ConcurrentDictionary<PropertyInfo, FastPropertyGetter>();

        static FastPropertyGetter()
        {
        }

        private FastPropertyGetter(PropertyInfo property)
            : base(property, true)
        {
        }

        public static FastPropertyGetter For(PropertyInfo property)
        {
            FastPropertyGetter addValue = (FastPropertyGetter)null;
            if (!FastPropertyGetter.propertyGetterMap.TryGetValue(property, out addValue))
            {
                addValue = new FastPropertyGetter(property);
                FastPropertyGetter.propertyGetterMap.AddOrUpdate(property, addValue, (Func<PropertyInfo, FastPropertyGetter, FastPropertyGetter>)((k, g) => g));
            }
            return addValue;
        }
    }
}
