using Microsoft.AspNetCore.Mvc;
using Rise.Application.Contracts.Managers.ContactInformation;
using Rise.Application.Contracts.Managers.ContactInformation.Dtos;
using SDIKit.Common.Attributes;
using SDIKit.Common.Enums;
using SDIKit.Common.Infrastructure;
using SDIKit.Common.Types.Result;
using System.Threading;
using System.Threading.Tasks;

namespace RiseWebApi.Controllers
{
    public class ContactInformationController : SharedBaseController
    {
        private readonly IContactInformationManager _contactInformationManager;

        public ContactInformationController(IContactInformationManager contactInformationManager)
        {

            _contactInformationManager = contactInformationManager;
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll([FromBody] ContactInformationFilterDto filter, CancellationToken cancellationToken)
        {
            var result = await _contactInformationManager.GetAll(filter, cancellationToken);
            return ApiResult(result);
        }
        [HttpPost("save")]
        [ValidateModel]
        public async Task<IActionResult> Save([FromBody] CreateOrEditContactInformationInput input)
        {
            if (input.IsNew)
                await _contactInformationManager.Add(input);
            else
                await _contactInformationManager.Update(input);

            return ApiResult(new ApiResult(resultState: ResultState.Info));
        }

        [HttpPost("delete")]
        [ValidateModel]
        public async Task<IActionResult> Delete([FromBody] long id)
        {
            await _contactInformationManager.Delete(id);
            return ApiResult(state: ResultState.Info);
        }

        [HttpPost("insert-dummy-data")]
        [ValidateModel]
        public async Task<IActionResult> InsertDummyData()
        {
            await _contactInformationManager.InsertDummyContactInformationData();
            return ApiResult();
        }

    }
}
