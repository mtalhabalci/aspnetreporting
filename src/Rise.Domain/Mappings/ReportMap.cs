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
    public class ReportMap : EntityBaseMap<Report>
    {
        public override void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("Reports");
            builder.HasData(new Report
            {
                Id = 1
                
            });
            base.Configure(builder);
        }
    }

}
