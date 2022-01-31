using SDIKit.Common.Helpers;

using System;
using System.Collections.Generic;

namespace SDIKit.Common.Types
{
    public class EnumModel
    {
        public EnumModel(Type type)
        {
            Values = new List<EnumData>();
            Name = type.FullName;
            foreach (Enum value in Enum.GetValues(type))
            {
                Values.Add(new EnumData
                {
                    Value = value.GetHashCode(),
                    Name = value.ToString(),
                    Display = value.GetDisplayName()
                });
            }
        }

        public string Name { get; }
        public List<EnumData> Values { get; }
    }
}