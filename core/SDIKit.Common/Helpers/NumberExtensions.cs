using System;
using System.Collections.Generic;
using System.Text;

namespace SDIKit.Common.Helpers
{
    public static class NumberExtensions
    {
        public static int RandomNumber(int min, int max)
        {
            Random random = new Random(); return random.Next(min, max);

        }
    }
}
