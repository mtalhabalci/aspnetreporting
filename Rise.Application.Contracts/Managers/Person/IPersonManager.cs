using Rise.Application.Contracts.Managers.Person.Dtos;
using SDIKit.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rise.Application.Contracts.Managers
{
    public interface IPersonManager
    {
        Task<IPagedList<PersonOutput>> GetAll(PersonFilterDto request, CancellationToken cancellationToken);
        Task Update(CreateOrEditPersonInput input);
        Task Add(CreateOrEditPersonInput input);
        Task Delete(long id);
        Task<PersonOutput> GetById(long id);
        Task InsertDummyContactInformationData();
    }
}
