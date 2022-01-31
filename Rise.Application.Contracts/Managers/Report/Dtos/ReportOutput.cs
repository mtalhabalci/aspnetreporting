using Rise.Application.Contracts.Types.Enums;
using SDIKit.Common.Attributes;
using SDIKit.Common.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Application.Contracts.Managers.Report.Dtos
{
    public class ReportOutput : DtoBase
    {
        [Display(Name = "Taleb EdilenTarih")]
        public DateTime RequestedDate { get; set; }
        [Display(Name = "Tamamlanma Tarihi")]
        public DateTime CompletedDate { get; set; }
        [Display(Name = "Dosya Yolu")]
        public string FilePath { get; set; }
        [Display(Name = "Rapor Durumu")]
        public string ReportStatusDisplayName { get; set; }
        [Hidden]
        public ReportStatusTypEnum ReportStatus { get; set; }
    }
}
