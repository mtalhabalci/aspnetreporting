using Rise.Application.Contracts.Types;
using Rise.Application.Contracts.Types.Enums;
using SDIKit.Common.Attributes;
using SDIKit.Common.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Application.Contracts.Managers.ContactInformation.Dtos
{
    public class ContactInformationOutput : DtoBase
    {
        [Display(Name="E-posta")]
        public string Email { get; set; }
        [Display(Name="Telefon")]
        public string Phone { get; set; }
        [Display(Name="Konum")]
        public string Location { get; set; }
    }
}
