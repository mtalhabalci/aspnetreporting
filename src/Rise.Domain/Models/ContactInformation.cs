using Rise.Application.Contracts.Types.Enums;

namespace Rise.Domain.Models
{
    public class ContactInformation : DefaultEntityBaseWithNavigation
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public long PersonId { get; set; }
        public Person Person { get; set; }
    }
}
