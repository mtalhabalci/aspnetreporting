using System.ComponentModel.DataAnnotations;

namespace SDIKit.Common.Enums
{
    public enum ResultState
    {
        [Display(Name = "unset")]
        Unset = 0,

        [Display(Name = "success")]
        Success = 1,

        [Display(Name = "warning")]
        Warning = 2,

        [Display(Name = "error")]
        Error = 3,

        [Display(Name = "info")]
        Info = 4
    }
}