using SDIKit.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Application.Contracts.Managers.Person.Dtos
{
    public class CreateOrEditPersonInput : DtoBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
    }
}
