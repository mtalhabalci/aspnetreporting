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
        [Display(Name = "Bilgi Tipi")]
        public string InformationTypeDisplayName { get; set; }
        [Hidden]
        public ContactTypeEnum InformationType { get; set; }
        [Display(Name="Değer")]
        public string Value { get; set; }
    }
}
