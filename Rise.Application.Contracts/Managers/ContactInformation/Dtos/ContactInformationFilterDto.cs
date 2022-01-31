using SDIKit.Common.UserInterfaceType.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Application.Contracts.Managers.ContactInformation.Dtos
{
    public class ContactInformationFilterDto : PageOptions
    {
        public long PersonId { get; set; }
    }
}
