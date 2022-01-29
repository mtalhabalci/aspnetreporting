using Microsoft.AspNetCore.Mvc;
using Rise.Application.Contracts.Managers;
using Rise.Application.Contracts.Managers.Person.Dtos;
using SDIKit.Common.Attributes;
using SDIKit.Common.Enums;
using SDIKit.Common.Infrastructure;
using SDIKit.Common.Types.Result;
using System.Threading;
using System.Threading.Tasks;

namespace RiseWebApi.Controllers
{
    public class PersonController : SharedBaseController
    {
        private readonly IPersonManager _personManager;

        public PersonController(IPersonManager personManager)
        {

            _personManager = personManager;
        }

        [HttpPost]
        public async Task<IActionResult> GetAllPersons([FromBody] PersonFilterDto filter, CancellationToken cancellationToken)
        {
            var result = await _personManager.GetAll(filter, cancellationToken);
            return ApiResult(result);
        }
        [HttpPost("save")]
        [ValidateModel]
        public async Task<IActionResult> Save([FromBody] CreateOrEditPersonInput input)
        {
            if (input.IsNew)
                await _personManager.Add(input);
            else
                await _personManager.Update(input);

            return ApiResult(new ApiResult(resultState: ResultState.Info));
        }
    }
}
