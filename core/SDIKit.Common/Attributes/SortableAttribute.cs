using System;

namespace SDIKit.Common.Attributes
{
    public class SortableAttribute : Attribute
    {
        public string Alias { get; private set; }

        public SortableAttribute(string alias = null)
        {
            Alias = alias;
        }
    }
}