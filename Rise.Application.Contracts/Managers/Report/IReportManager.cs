using Rise.Application.Contracts.Managers.Report.Dtos;
using SDIKit.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rise.Application.Contracts.Managers.Report
{
    public interface IReportManager
    {
        Task RequestReport();
        Task<IPagedList<ReportOutput>> GetAll(ReportFilterDto request, CancellationToken cancellationToken);
    }
}
