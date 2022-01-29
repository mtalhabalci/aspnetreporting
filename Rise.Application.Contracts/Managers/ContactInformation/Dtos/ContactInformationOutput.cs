using Rise.Application.Contracts.Types;
using SDIKit.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Application.Contracts.Managers.ContactInformation.Dtos
{
    public class ContactInformationOutput : DtoBase
    {
        public string InformationTypeDisplayName { get; set; }
        public ContactTypeEnum InformationType { get; set; }
        public string Value { get; set; }
    }
}
