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
               InformationType = k.ContactType,
               Value = k.Value,
               InformationTypeDisplayName = k.ContactType.GetDisplayName()
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
                ContactType = input.ContactType,
                PersonId = input.PersonId,
                Value = input.Value
            };
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    repo.Insert(newItem);
                    await _unitOfWork.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
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

            contactInformation.ContactType = input.ContactType;
            contactInformation.Value = input.Value;
            contactInformation.PersonId= input.PersonId;
            

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    repo.Update(contactInformation);

                    await _unitOfWork.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }


}
