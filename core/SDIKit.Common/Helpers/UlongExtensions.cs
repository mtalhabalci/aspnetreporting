using System;
using System.Collections.Generic;
using System.Text;

namespace SDIKit.Common.Helpers
{
  
    public static class UlongExtensions
    {
        public static ulong Sum(this List<ulong> value)
        {
            ulong result = 0;
            foreach (var item in value)
            {
                result += item;
            }
            return result;
        }
    }
}
