using Ybm.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.Framework.Reflection
{
    public static class ReflectionExtensions
    {
        //public static object GetValueFast(this PropertyInfo property, object target)
        //{
        //    try
        //    {
        //        return FastPropertyGetter.For(property).Invoke(target);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ReflectionExtensions.GetPropertyAccessException("get", property, target, ex);
        //    }
        //}

        //public static TValue GetValueFast<TValue>(this PropertyInfo property, object target)
        //{
        //    try
        //    {
        //        return FastPropertyGetter<TValue>.For(property).Invoke(target);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ReflectionExtensions.GetPropertyAccessException("get", property, target, ex);
        //    }
        //}

        //public static void SetValueFast(this PropertyInfo property, object target, object value)
        //{
        //    try
        //    {
        //        FastPropertySetter.For(property).Invoke(target, value);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ReflectionExtensions.GetPropertyAccessException("set", property, target, ex);
        //    }
        //}

        //public static void SetValueFast<TValue>(this PropertyInfo property, object target, TValue value)
        //{
        //    try
        //    {
        //        FastPropertySetter<TValue>.For(property).Invoke(target, value);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ReflectionExtensions.GetPropertyAccessException("set", property, target, ex);
        //    }
        //}

        //public static object InvokeFast(this MethodInfo method, object target, params object[] arguments)
        //{
        //    return FastMethodInvocation.For(method).Invoke(target, arguments);
        //}

        //public static TResult InvokeFast<TResult>(this MethodInfo method, object target, params object[] arguments)
        //{
        //    return FastMethodInvocation<TResult>.For(method).Invoke(target, arguments);
        //}

        //private static Exception GetPropertyAccessException(string accessType, PropertyInfo property, object target, Exception ex)
        //{
        //    StringBuilder stringBuilder = new StringBuilder();
        //    stringBuilder.Append("Unable to ").Append(accessType).Append(" property ");
        //    if (property != (PropertyInfo)null)
        //        stringBuilder.Append("'").Append(property.Name).Append("' on type '").Append(property.DeclaringType.ToString()).Append("'");
        //    else
        //        stringBuilder.Append("null");
        //    if (target == null)
        //        stringBuilder.Append(" because target object is null");
        //    return new Exception(((object)stringBuilder).ToString(), ex);
        //}
    }
}
