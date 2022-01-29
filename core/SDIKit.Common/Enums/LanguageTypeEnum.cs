using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SDIKit.Common.Enums
{
    public enum LanguageTypeEnum
    {
        [Display(Name = "tr")]
        [Description("tr")]
        tr = 1,

        [Display(Name = "en")]
        [Description("en")]
        en = 2,

        [Display(Name = "es")]
        [Description("es")]
        es = 3,

        [Display(Name = "fr")]
        [Description("fr")]
        fr = 4,

        [Display(Name = "de")]
        [Description("de")]
        de = 5,

        [Display(Name = "it")]
        [Description("it")]
        it = 6,
    }
}
