using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.Framework.Reflection
{
    public static class ReflectionHelper
    {
        public static bool HasMethodTheAttribute<T>(MethodInfo methodInfo)
        {
            return methodInfo.IsDefined(typeof(T), true);
        }
        public static bool HasPropertyTheAttribute<T>(PropertyInfo propertyInfo)
        {
            return propertyInfo.IsDefined(typeof(T), true);
        }


        public const BindingFlags fullAccess = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        public static bool IsNullable(this Type type)
        {
            return !type.IsValueType || Nullable.GetUnderlyingType(type) != (Type)null;
        }

        public static void SetFieldValue(object obj, string fieldName, object value)
        {
            obj.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).SetValue(obj, value);
        }

        //public static void SetPropertyValue(object obj, string propertyName, object value)
        //{
        //    if (obj == null)
        //        throw new ArgumentNullException("Object");
        //    PropertyInfo property = obj.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        //    if (property == (PropertyInfo)null)
        //        throw new ArgumentException(string.Format("Property name '{0}' was not found on type '{1}'", (object)propertyName, (object)obj.GetType()));
        //    ReflectionExtensions.SetValueFast(property, obj, value);
        //}

        //public static void SetMemebrValue(object obj, string memberName, object value, MemberTypes memberType)
        //{
        //    switch (memberType)
        //    {
        //        case MemberTypes.Field:
        //            ReflectionHelper.SetFieldValue(obj, memberName, value);
        //            break;
        //        case MemberTypes.Property:
        //            ReflectionHelper.SetPropertyValue(obj, memberName, value);
        //            break;
        //    }
        //}

        public static object GetFieldValue(object obj, string fieldName)
        {
            return obj.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(obj);
        }

        public static T GetFieldValue<T>(object obj, string propertyName)
        {
            return (T)ReflectionHelper.GetFieldValue(obj, propertyName);
        }

        //public static object GetPropertyValue(object obj, string propertyName)
        //{
        //    if (obj == null)
        //        throw new ArgumentNullException("Object");
        //    PropertyInfo property = obj.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        //    if (property == (PropertyInfo)null)
        //        throw new ArgumentException(string.Format("Property name '{0}' was not found on type '{1}'", (object)propertyName, (object)obj.GetType()));
        //    else
        //        return ReflectionExtensions.GetValueFast(property, obj);
        //}

        //public static T GetPropertyValue<T>(object obj, string propertyName)
        //{
        //    return (T)ReflectionHelper.GetPropertyValue(obj, propertyName);
        //}

        //public static object GetMemberValue(object obj, string memberName, MemberTypes memberType)
        //{
        //    switch (memberType)
        //    {
        //        case MemberTypes.Field:
        //            return ReflectionHelper.GetFieldValue(obj, memberName);
        //        case MemberTypes.Property:
        //            return ReflectionHelper.GetPropertyValue(obj, memberName);
        //        default:
        //            return (object)null;
        //    }
        //}

        public static Exception UnwrapException(Exception ex)
        {
            Exception exception = ex;
            while (exception is TargetInvocationException)
                exception = exception.InnerException;
            return exception ?? ex;
        }

        public static Exception UnwrapException(Exception ex, Func<Exception, bool> finder)
        {
            for (Exception exception = ex; exception != null; exception = exception.InnerException)
            {
                if (finder(exception))
                    return exception;
            }
            return (Exception)null;
        }

        public static Type UnwrapNullableType(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return type.GetGenericArguments()[0];
            else
                return type;
        }
    }
}
