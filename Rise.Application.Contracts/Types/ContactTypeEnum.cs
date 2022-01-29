using System.ComponentModel.DataAnnotations;

namespace Rise.Application.Contracts.Types
{
    public enum ContactTypeEnum : int
    {
        [Display(Name = "Phone")]
        Phone = 1,

        [Display(Name = "Email")]
        Email = 2,

        [Display(Name = "Location")]
        Location = 3
    }
}
