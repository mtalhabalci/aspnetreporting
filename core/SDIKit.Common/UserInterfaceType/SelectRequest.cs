using System;
using System.Collections.Generic;
using System.Linq;

namespace SDIKit.Common.UserInterfaceType
{
    public class SelectRequest
    {
        public SelectRequest()
        {
            PageSize = 15;
        }

        public string SearchTerm { get; set; }
        public List<SelectItem> SelectedItems { get; set; }
        public int PageSize { get; set; }
        public int ParentId { get; set; }

        public List<long> GetSelectedItems()
        {
            return SelectedItems?.Select(k => Convert.ToInt64(k.Id)).ToList();
        }
    }
}