using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Application.Contracts.Managers.Report.Dtos
{
    public class ReportResultOutput
    {
        public string Location { get; set; }
        public long PersonCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
