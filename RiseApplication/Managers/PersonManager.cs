using Rise.Application.Contracts.Managers;
using Rise.Application.Contracts.Managers.Person.Dtos;
using Rise.Domain.Models;
using SDIKit.Common.Helpers;
using SDIKit.Common.Interfaces;
using SDIKit.Data.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Rise.Application.Managers
{
    public class PersonManager : IPersonManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public PersonManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IPagedList<PersonOutput>> GetAll(PersonFilterDto request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<Person>();

            #region Select

            Expression<Func<Person, PersonOutput>> select = k => new PersonOutput()
            {
                Name = k.Name,
                Surname = k.Surname,
                Company = k.Company,
                Id = k.Id
            };

            #endregion Select

            #region Predicate

            Expression<Func<Person, bool>> predicate = x =>true;

            #endregion Predicate


            var result = await repo.GetPagedListAsync(selector: select
                , predicate: predicate
                , pageIndex: request.PageNumber - 1
                , pageSize: request.RowCount
                , cancellationToken: cancellationToken);

            return result;
        }
        public async Task Add(CreateOrEditPersonInput input)
        {
            var repo = _unitOfWork.Repository<Person>();

            var newItem = new Person()
            {
                Name = input.Name,
                Surname = input.Surname,
                Company = input.Company
            };
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    repo.Insert(newItem);
                    await _unitOfWork.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task Update(CreateOrEditPersonInput input)
        {
            var repo = _unitOfWork.Repository<Person>();

            var person = await repo.FindAsync(input.Id);

            person.Name = input.Name;
            person.Surname = input.Surname;
            person.Company = input.Company;

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    repo.Update(person);

                    await _unitOfWork.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task Delete(long id)
        {
            var repo = _unitOfWork.Repository<Person>();
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    repo.Delete(id);
                    await _unitOfWork.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<PersonOutput> GetById(long id)
        {
            var repo = _unitOfWork.Repository<Person>();
            var person = await repo.FindAsync(id);
            var personOutput = new PersonOutput()
            {
                Id = person.Id,
                Name = person.Name,
                Surname = person.Surname,
                Company = person.Company
            };
            return personOutput;
        }

        public async Task InsertDummyContactInformationData()
        {
            var repo = _unitOfWork.Repository<Person>();
            var lipsum = new LipsumGeneratorHelper();
            for (int i = 0; i < 100; i++)
            {

                var contactInfo = new Person
                {
                   Company = lipsum.NextLoremIpsum(1),
                   Name = lipsum.NextLoremIpsum(1),
                   Surname = lipsum.NextLoremIpsum(1)
                };

                repo.Insert(contactInfo);
            }
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    await _unitOfWork.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
