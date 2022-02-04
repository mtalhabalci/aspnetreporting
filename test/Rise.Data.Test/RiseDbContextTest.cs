using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rise.Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Rise.Data.Test
{
    [TestClass]
    public class RiseDbContextTest : UnitTestBase
    {
        [DataTestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public async Task CanConnectDatabase_returns_true(bool async)
        {
            Assert.IsTrue(async ? await UnitOfWork.CanConnectAsync() : UnitOfWork.CanConnect());
        }


        [TestMethod]
        public void Connect()
        {
            var repo = UnitOfWork.Repository<Person>();
            Assert.IsTrue(repo != null);
        }

        [TestMethod]
        public void UnitOfWork_query_firstOrDefault()
        {
            var album = UnitOfWork.Repository<Person>().GetFirst(i => i.Id == 1);
            Assert.IsNotNull(album);
        }

        [TestMethod]
        public void UnitOfWork_can_insert()
        {

            var person = new Person
            {
                Name = LoremIpsum.NextLoremIpsum(1),
                Surname = LoremIpsum.NextLoremIpsum(1),
                Company = LoremIpsum.NextLoremIpsum(1)
            };

            var repo = UnitOfWork.Repository<Person>();
            repo.Insert(person);

            var result = UnitOfWork.SaveChanges();
            Assert.IsTrue(result > default(int));
        }

        [TestMethod]
        public void UnitOfWork_can_insert_with_transaction_commit()
        {
            var person = new Person
            {
                Name = LoremIpsum.NextLoremIpsum(1),
                Surname = LoremIpsum.NextLoremIpsum(1),
                Company = LoremIpsum.NextLoremIpsum(1)
            };
            var repo = UnitOfWork.Repository<Person>();

            using (var transaction = UnitOfWork.BeginTransaction())
            {
                try
                {
                    repo.Insert(person);
                    var result = UnitOfWork.SaveChanges();
                    transaction.Commit();
                    Assert.IsTrue(result > default(int));
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Assert.Fail(ex.Message);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Transaction insert rollback.")]
        public void UnitOfWork_can_insert_with_transaction_rollback()
        {
            var person = new Person
            {
                Name = LoremIpsum.NextLoremIpsum(1),
                Surname = LoremIpsum.NextLoremIpsum(1),
                Company = LoremIpsum.NextLoremIpsum(1)
            };
            var repo = UnitOfWork.Repository<Person>();

            using (var transaction = UnitOfWork.BeginTransaction())
            {
                try
                {
                    repo.Insert(person);
                    var result = UnitOfWork.SaveChanges();
                    throw new Exception("Transaction insert rollback.");
                    transaction.Commit();
                    Assert.IsTrue(result > default(int));
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }


        [TestMethod]
        public void UnitOfWork_can_update()
        {
            var id = 1;
            var date = DateTime.Now.ToString();

            var albumRepo = UnitOfWork.Repository<Person>();
            var album = albumRepo.GetFirst(i => i.Id == id);
            album.Name = LoremIpsum.NextLoremIpsum(1);
            album.Surname = $"{nameof(UnitOfWork_can_update)} - {date}";
            albumRepo.Update(album);

            var result = UnitOfWork.SaveChanges();
            var updatedAlbum = albumRepo.GetFirst(i => i.Id == id);
            Assert.IsTrue(result > default(int) && updatedAlbum.Surname.EndsWith(date));
        }

        [TestMethod]
        public void UnitOfWork_can_update_with_transaction_commit()
        {
            var id = 1;
            var date = DateTime.Now.ToString();

            var personRepo = UnitOfWork.Repository<Person>();
            var person = personRepo.GetFirst(i => i.Id == id);
            person.Name = LoremIpsum.NextLoremIpsum(1);
            person.Surname = $"{nameof(UnitOfWork_can_update_with_transaction_commit)} - {date}";
            personRepo.Update(person);

            int result;
            using (var transaction = UnitOfWork.BeginTransaction())
            {
                try
                {
                    result = UnitOfWork.SaveChanges();
                    transaction.Commit();
                    Assert.IsTrue(result > default(int));
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Assert.Fail(ex.Message);
                }
            }

            var updatedPerson = personRepo.GetFirst(i => i.Id == id);
            Assert.IsTrue(updatedPerson.Surname.EndsWith(date));
        }


        [TestMethod]
        [ExpectedException(typeof(Exception), "Transaction update rollback.")]
        public void UnitOfWork_can_update_with_transaction_rollback()
        {
            var id = 1;
            var date = DateTime.Now.ToString();

            var personRepo = UnitOfWork.Repository<Person>();
            var album = personRepo.GetFirst(i => i.Id == id);
            album.Name = LoremIpsum.NextLoremIpsum(1);
            album.Surname = $"{nameof(UnitOfWork_can_update_with_transaction_rollback)} - {date}";

            int result;
            using (var transaction = UnitOfWork.BeginTransaction())
            {
                try
                {
                    personRepo.Update(album);
                    result = UnitOfWork.SaveChanges();
                    throw new Exception("Transaction update rollback.");
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        [TestMethod]
        public async Task UnitOfWork_can_delete()
        {
            var repo = UnitOfWork.Repository<Person>();
            var person = await repo.GetFirstAsync(x => x.Id != 1);
            if (person == null)
                Assert.Fail("Silinecek albüm bulunamadý!", person);
            repo.Delete(person.Id);
            var result = UnitOfWork.SaveChanges();
            Assert.IsTrue(result > default(int));
        }


        [TestMethod]
        public void UnitOfWork_with_ordered()
        {
            var album = UnitOfWork.Repository<Person>().GetFirst(orderBy: k => k.OrderBy(y => y.Id));
            Assert.IsTrue(album.Id == 1);
        }

        [TestMethod]
        public void UnitOfWork_with_include()
        {
            var person = UnitOfWork.Repository<Person>().GetFirst(include: k => k.Include(y => y.ContactInformations));
            Assert.IsTrue(person.ContactInformations != null);
        }

        [TestMethod]
        public void UnitOfWork_with_pagination()
        {
            var pagedList = UnitOfWork.Repository<Person>().GetPagedList(include: k => k.Include(y => y.ContactInformations));
            Assert.IsTrue(pagedList.TotalCount > default(long));
        }
    }
}
