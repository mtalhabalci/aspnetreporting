using Rise.Application.Contracts.Managers.ContactInformation;
using Rise.Application.Contracts.Managers.ContactInformation.Dtos;
using Rise.Application.Contracts.Managers.Person.Dtos;
using Rise.Domain.Models;
using SDIKit.Common.Helpers;
using SDIKit.Common.Interfaces;
using SDIKit.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rise.Application.Managers
{
    public class ContactInformationManager : IContactInformationManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public ContactInformationManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IPagedList<ContactInformationOutput>> GetAll(ContactInformationFilterDto request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<ContactInformation>();

            #region Select

            Expression<Func<ContactInformation, ContactInformationOutput>> select = k => new ContactInformationOutput()
            {
                Id = k.Id,
                Email = k.Email,
                Phone = k.Phone,
                Location = k.Location
            };

            #endregion Select

            #region Predicate

            Expression<Func<ContactInformation, bool>> predicate = x => x.PersonId == request.PersonId;

            #endregion Predicate


            var result = await repo.GetPagedListAsync(selector: select
                , predicate: predicate
                , pageIndex: request.PageNumber - 1
                , pageSize: request.RowCount
                , cancellationToken: cancellationToken);

            return result;
        }
        public async Task Add(CreateOrEditContactInformationInput input)
        {
            var repo = _unitOfWork.Repository<ContactInformation>();

            var newItem = new ContactInformation()
            {
                Email = input.Email,
                Location = input.Location,
                Phone = input.Phone,
                PersonId = input.PersonId,
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

        public async Task Update(CreateOrEditContactInformationInput input)
        {
            var repo = _unitOfWork.Repository<ContactInformation>();

            var contactInformation = await repo.FindAsync(input.Id);

            contactInformation.Phone = input.Phone;
            contactInformation.Email = input.Email;
            contactInformation.Location = input.Location;
            contactInformation.PersonId = input.PersonId;


            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    repo.Update(contactInformation);

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
            var repo = _unitOfWork.Repository<ContactInformation>();
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
        private long GetRandomUserId(List<long> personIds)
        {
            Random r = new Random();
            return Convert.ToInt64(personIds.OrderBy(x => r.Next()).Take(1).FirstOrDefault());
        }
        public async Task InsertDummyContactInformationData()
        {
            var repo = _unitOfWork.Repository<ContactInformation>();
            var lipsum = new LipsumGeneratorHelper();
            var repoPersons = _unitOfWork.Repository<Person>();
            var persons = await repoPersons.GetAndMapAllAsync(selector: x => x.Id);
            for (int i = 0; i < 100000; i++)
            {

                var contactInfo = new ContactInformation
                {

                    Email = lipsum.NextLoremIpsum(1),
                    Location = lipsum.NextLoremIpsum(1),
                    Phone = lipsum.NextLoremIpsum(1),
                    PersonId = GetRandomUserId(persons)
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
