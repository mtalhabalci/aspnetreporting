using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rise.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Domain.Mappings
{

    public class ContactInformationMap : EntityBaseMap<ContactInformation>
    {
        public override void Configure(EntityTypeBuilder<ContactInformation> builder)
        {
            builder.ToTable("ContactInformations");

            builder.HasOne(d => d.Person)
               .WithMany(p => p.ContactInformations)
               .HasForeignKey(d => d.PersonId)
               .OnDelete(DeleteBehavior.Restrict)
               .HasConstraintName("FK_ContactInformation_PersonId_Person_Id"); //contactinfo'nun personId'si --> person'ın id'si 

            builder.HasData(new ContactInformation
            {
                Id = 1,
                Email = "test@test.com",
                Phone = "+905345860966",
                Location = "Ankara",
                PersonId = 1
            });

            base.Configure(builder);
        }
    }
}
