using Rise.Application.Contracts.Types;

namespace Rise.Domain.Models
{
    public class ContactInformation : DefaultEntityBaseWithNavigation
    {
        public ContactTypeEnum ContactType { get; set; }
        public string Value { get; set; }
        public long PersonId { get; set; }
        public Person Person { get; set; }
    }
}
