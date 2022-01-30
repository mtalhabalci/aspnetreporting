using System.ComponentModel.DataAnnotations;

namespace Rise.Application.Contracts.Types.Enums
{
    public enum ContactTypeEnum : int
    {
        [Display(Name = "Telefon")]
        Phone = 1,

        [Display(Name = "Eposta")]
        Email = 2,

        [Display(Name = "Konum")]
        Location = 3
    }
}
