using System;
using System.Collections.Generic;
using System.Text;

namespace SDIKit.Common.Identity
{
    public class RequestHeaderInformation
    {
        public string Language { get; set; }
        public string TimeZone { get; set; }
        public long UserId { get; set; }
    }
}
