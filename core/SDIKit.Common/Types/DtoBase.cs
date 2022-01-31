using SDIKit.Common.Attributes;
using SDIKit.Common.Entity;

using System;
using System.ComponentModel.DataAnnotations;

namespace SDIKit.Common.Types
{
    public class DtoBase : IDtoBase<long>
    {
        public DtoBase()
        {
            var dateTimeNow = DateTime.Now;
            Created = dateTimeNow;
            IsActive = true;
        }

        [Display(Name = "Kimlik")]
        [Hidden]
        public long Id { get; set; }

        [Display(Name = "Kayıt Tarihi")]
        [Hidden]
        public DateTime Created { get; set; }

        [Display(Name = "Kayıt Eden Kişi")]
        [Hidden]
        public long? CreatedBy { get; set; }

        [Display(Name = "Güncelleme Tarihi")]
        [Hidden]
        public DateTime? Modified { get; set; }

        [Display(Name = "Güncelleyen Kişi")]
        [Hidden]
        public long? ModifiedBy { get; set; }

        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }

        [Hidden]
        public bool IsNew => Id == 0;
    }
}