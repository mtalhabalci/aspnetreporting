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
            builder.Property(e => e.Name).HasMaxLength(50).HasConversion(k => k.Trim(), k => k);
            builder.Property(e => e.Company).HasMaxLength(50).HasConversion(k => k.Trim(), k => k);
            builder.Property(e => e.Surname).HasMaxLength(50).HasConversion(k => k.Trim(), k => k);



            base.Configure(builder);
        }
    }
}
