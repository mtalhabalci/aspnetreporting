using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rise.Application.Contracts.Types;
using Rise.Application.Contracts.Types.Enums;
using Rise.Domain;

namespace Rise.Domain.Models
{
    public class Report : DefaultEntityBaseWithNavigation
    {
        public DateTime RequestedDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public string FilePath { get; set; }
        public ReportStatusTypEnum ReportStatus { get; set; }
    }
}
