using System;
using System.Reflection;

namespace SDIKit.Common.Extensions
{
    public static class TypeExtensions
    {
        public static Type BaseType(this Type type)
        {
            return type.GetTypeInfo().BaseType;
        }

        public static bool IsEnumType(this Type type)
        {
            return IntrospectionExtensions.GetTypeInfo(type.GetNonNullableType()).IsEnum;
        }

        public static Type GetNonNullableType(this Type type)
        {
            if (!type.IsNullableType())
                return type;

            return type.GetGenericArguments()[0];
        }

        public static bool IsNullableType(this Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }

        public static object GetDefaultValue(this Type t)
        {
            if (t.IsValueType && Nullable.GetUnderlyingType(t) == null)
                return Activator.CreateInstance(t);
            else
                return null;
        }
    }
}