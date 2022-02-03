using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rise.Domain;

namespace Rise.Domain.Models
{
    public class Person : DefaultEntityBaseWithNavigation
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public ICollection<ContactInformation> ContactInformations { get; set; }

    }
}
