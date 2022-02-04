using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Application.Contracts.Types.Enums
{
    public enum ReportStatusTypEnum
    {
        [Display(Name = "Devam Ediyor")]
        Ongoing = 1,

        [Display(Name = "Tamamlandı")]
        Over = 2
    }
}
