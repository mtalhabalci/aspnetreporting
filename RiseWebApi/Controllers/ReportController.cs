using Microsoft.AspNetCore.Mvc;
using Rise.Application.Contracts.Managers.Report;
using Rise.Application.Contracts.Managers.Report.Dtos;
using SDIKit.Common.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace RiseWebApi.Controllers
{
    public class ReportController : SharedBaseController
    {
        private readonly IReportManager _reportManager;

        public ReportController(IReportManager reportManager)
        {

            _reportManager = reportManager;
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAllPersons([FromBody] ReportFilterDto filter, CancellationToken cancellationToken)
        {
            var result = await _reportManager.GetAll(filter, cancellationToken);
            return ApiResult(result);
        }

        [HttpGet("request-report")]
        public async Task<IActionResult> RequestReport()
        {
            await _reportManager.RequestReport();
            return ApiResult();
        }
    }
}
