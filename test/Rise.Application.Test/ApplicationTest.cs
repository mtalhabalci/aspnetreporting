using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rise.Application.Contracts.Managers.Person.Dtos;
using Rise.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Application.Test
{
    [TestClass]

    public class ApplicationTest : ApplicationUnitTestBase
    {
        [DataTestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public async Task CanAddPersonWithManager_returns_true(bool async)
        {
            var person = new CreateOrEditPersonInput
            {
                Name = LoremIpsum.NextLoremIpsum(1),
                Surname = LoremIpsum.NextLoremIpsum(1),
                Company = LoremIpsum.NextLoremIpsum(1)
            };
            try
            {
                await PersonManager.Add(person);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
                throw;
            }
        }
        [DataTestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public async Task CanGetPersonById_returns_true(bool async)
        {
            var person = await PersonManager.GetById(1);
            Assert.IsTrue(person.Id == 1);
        }

        [DataTestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public async Task CanUpdatePerson_returns_true(bool async)
        {
            var person = await PersonManager.GetById(1);

            try
            {
                await PersonManager.Update(new CreateOrEditPersonInput
                {
                    Name = LoremIpsum.NextLoremIpsum(1),
                    Surname = LoremIpsum.NextLoremIpsum(1),
                    Company = LoremIpsum.NextLoremIpsum(1),
                    Id = 1
                });
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
                throw;
            }
        }

        [DataTestMethod]
        public async Task CanDeletePerson_returns_true()
        {
            var person = await UnitOfWork.Repository<Person>().GetFirstAsync(x => x.Id != 1);
            try
            {
                await PersonManager.Delete(person.Id);
               var deletedPerson = await PersonManager.GetById(person.Id);
                Assert.IsTrue(deletedPerson == null);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
                throw;
            }
        }

        [DataTestMethod]
        public async Task CanRequestReport_returns_true()
        {
            try
            {
                var reportCount = await UnitOfWork.Repository<Report>().Count();
                await ReportManager.RequestReport();
                var reportCount2 = await UnitOfWork.Repository<Report>().Count();

                Assert.IsTrue(reportCount != reportCount2);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
                throw;
            }
        }

    }
}
