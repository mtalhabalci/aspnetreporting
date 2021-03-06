using System.Collections.Generic;

namespace SDIKit.Common.UserInterfaceType
{
    public class SelectResult
    {
        public SelectResult()
        {
            Results = new List<SelectItem>();
        }

        public int Total { get; set; }
        public List<SelectItem> Results { get; set; }
    }
}