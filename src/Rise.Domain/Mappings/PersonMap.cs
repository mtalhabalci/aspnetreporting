using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rise.Domain.Models;

namespace Rise.Domain.Mappings
{
    public class PersonMap : EntityBaseMap<Person>
    {
        public override void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(e => e.Name).HasConversion(k => k.Trim(), k => k);
            builder.Property(e => e.Company).HasConversion(k => k.Trim(), k => k);
            builder.Property(e => e.Surname).HasConversion(k => k.Trim(), k => k);

            builder.HasData(new Person
            {
                Id = 1,
                Name = "test",
                Company = "test company",
                Surname = "test surname"
            });


            base.Configure(builder);
        }
    }
}
