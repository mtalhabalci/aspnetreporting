using Rise.Application.Contracts.Types;
using SDIKit.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Application.Contracts.Managers.ContactInformation.Dtos
{
    public class CreateOrEditContactInformationInput : DtoBase
    {
        public long PersonId { get; set; }
        public string Value { get; set; }
        public ContactTypeEnum ContactType { get; set; }
    }
}
