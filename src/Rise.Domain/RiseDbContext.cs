using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Rise.Domain.Mappings;
using Rise.Domain.Models;
using SDIKit.Common;
using SDIKit.Common.Identity;
using SDIKit.Data;
using SDIKit.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Domain
{
    public class RiseDbContext : DbContextBase
    {
        private readonly IOptions<DatabaseSettings> _databaseSettings;

        public RiseDbContext([NotNull] DbContextOptions options, IOptions<DatabaseSettings> databaseSettings) : base(options, databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        #region DbSets

        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<ContactInformation> ContactInformations { get; set; }
        public virtual DbSet<Report> Reports{ get; set; }


        #endregion DbSets

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(new PersonMap().Configure);
            modelBuilder.Entity<Report>(new ReportMap().Configure);
            modelBuilder.Entity<ContactInformation>(new ContactInformationMap().Configure);
           

            base.OnModelCreating(modelBuilder);
        }
    }
}
