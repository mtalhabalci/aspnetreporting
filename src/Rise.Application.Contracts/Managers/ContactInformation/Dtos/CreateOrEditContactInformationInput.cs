using Rise.Application.Contracts.Types;
using Rise.Application.Contracts.Types.Enums;
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
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
    }
}
