using Rise.Application.Contracts.Managers.ContactInformation.Dtos;
using Rise.Application.Contracts.Managers.Person.Dtos;
using SDIKit.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rise.Application.Contracts.Managers.ContactInformation
{
    public interface IContactInformationManager
    {
        Task<IPagedList<ContactInformationOutput>> GetAll(ContactInformationFilterDto request, CancellationToken cancellationToken);
        Task Add(CreateOrEditContactInformationInput input);
        Task Update(CreateOrEditContactInformationInput input);
    }
}
