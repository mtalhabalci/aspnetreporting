using System;

namespace SDIKit.Common.Attributes
{
    public class UnitAttribute : Attribute
    {
        public string Unit { get; set; }

        public UnitAttribute(string unit)
        {
            Unit = unit;
        }
    }
}