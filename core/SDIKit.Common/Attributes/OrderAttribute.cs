using System;

namespace SDIKit.Common.Attributes
{
    public class OrderAttribute : Attribute
    {
        public int ColumnIndex { get; set; }

        public OrderAttribute(int index)
        {
            ColumnIndex = index;
        }
    }
}