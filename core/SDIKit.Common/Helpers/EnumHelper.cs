using SDIKit.Common.Extensions;
using SDIKit.Common.Types;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SDIKit.Common.Helpers
{
    public static class EnumHelper
    {
        internal static bool HasFlags(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            Type checkedType = Nullable.GetUnderlyingType(type) ?? type;
            return HasFlagsInternal(checkedType);
        }

        private static bool HasFlagsInternal(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            FlagsAttribute attribute = type.GetTypeInfo().GetCustomAttribute<FlagsAttribute>(inherit: false);

            return attribute != null;
        }

        private static string GetDisplayName(FieldInfo field)
        {
            DisplayAttribute display = field.GetCustomAttribute<DisplayAttribute>(inherit: false);
            if (display != null)
            {
                string name = display.Name;
                if (!string.IsNullOrEmpty(name))
                {
                    return name;
                }
            }

            return field.Name;
        }
        public static string GetDescription(this Enum instance)
        {
            if (instance == null)
                return string.Empty;
            var fieldInfo = instance.GetType().GetField(instance.ToString());
            return GetDescription(fieldInfo);

        }
        public static string GetDescription(FieldInfo field)
        {
            DescriptionAttribute display = field.GetCustomAttribute<DescriptionAttribute>(inherit: false);
            if (display != null)
            {
                string name = display.Description;
                if (!string.IsNullOrEmpty(name))
                {
                    return name;
                }
            }
            return field.Name;
        }
        public static string GetDisplayName(this Enum instance)
        {
            if (instance == null)
                return string.Empty;
            var fieldInfo = instance.GetType().GetField(instance.ToString());
            return GetDisplayName(fieldInfo);
        }

        public static string GetDisplayName<TEnum>(TEnum instance) where TEnum : struct, IConvertible
        {
            var fieldInfo = instance.GetType().GetField(instance.ToString());
            return GetDisplayName(fieldInfo);
        }

        public static IList<string> AsStringList<TEnum>(IList<TEnum> enums) where TEnum : struct, IConvertible
        {
            if (!typeof(TEnum).IsEnumType())
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            IList<string> resultList = new List<string>();
            foreach (var item in enums)
            {
                var parsedEnum = Enum.Parse(typeof(TEnum), item.ToString());
                resultList.Add(EnumHelper.GetDisplayName((Enum)parsedEnum));
            }

            return resultList;
        }

        public static IList<IdTextPair> AsIdTextPairList<TEnum>(IList<TEnum> enums) where TEnum : struct, IConvertible
        {
            if (!typeof(TEnum).IsEnumType())
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            IList<IdTextPair> resultList = new List<IdTextPair>();
            foreach (var item in enums)
            {
                var parsedEnum = Enum.Parse(typeof(TEnum), item.ToString());
                resultList.Add(new IdTextPair { Id = item.GetHashCode().ToString(), Text = item.ToString(), Optional = EnumHelper.GetDisplayName((Enum)parsedEnum) });
            }

            return resultList;
        }

        /// <summary>
        /// Unique Flags Enum Converter
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="enumCommaList"></param>
        /// <returns></returns>
        public static TEnum ConvertEnum<TEnum>(string commaListed) where TEnum : struct
        {
            if (string.IsNullOrEmpty(commaListed))
            {
                throw new ArgumentNullException(nameof(commaListed));
            }

            return (TEnum)Enum.Parse(typeof(TEnum), commaListed);
        }
    }
}