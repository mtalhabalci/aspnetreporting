using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Application.Contracts.Types
{
    public enum ReportStatusTypEnum
    {
        [Display(Name = "Ongoing")]
        Ongoing = 1,

        [Display(Name = "Over")]
        Over = 2
    }
}
