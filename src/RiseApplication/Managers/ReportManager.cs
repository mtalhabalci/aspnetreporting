using Rise.Application.Contracts.Managers.Report;
using Rise.Application.Contracts.Managers.Report.Dtos;
using Rise.Domain.Models;
using Rise.Rabbitmq;
using Rise.Rabbitmq.Models;
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
    public class ReportManager : IReportManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRabbitmqPost _rabbitmqPost;

        public ReportManager(IUnitOfWork unitOfWork, IRabbitmqPost rabbitmqPost)
        {
            _unitOfWork = unitOfWork;
            _rabbitmqPost = rabbitmqPost;

        }

        public async Task<IPagedList<ReportOutput>> GetAll(ReportFilterDto request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<Report>();

            #region Select

            Expression<Func<Report, ReportOutput>> select = k => new ReportOutput()
            {
                FilePath = k.FilePath,
                ReportStatus = k.ReportStatus,
                ReportStatusDisplayName = k.ReportStatus.GetDisplayName(),
                RequestedDate = k.RequestedDate,
                CompletedDate = k.CompletedDate,
                Id = k.Id
            };

            #endregion Select

            #region Predicate

            Expression<Func<Report, bool>> predicate = x => true;

            #endregion Predicate


            var result = await repo.GetPagedListAsync(selector: select
                , predicate: predicate
                , pageIndex: request.PageNumber - 1
                , pageSize: request.RowCount
                , cancellationToken: cancellationToken);

            return result;
        }


        public async Task HandleReportIsCompleted(long reportId, string filePath)
        {
            var repoReport = _unitOfWork.Repository<Report>();
            var report = await repoReport.GetFirstAsync(x => x.Id == reportId);
            report.ReportStatus = Contracts.Types.Enums.ReportStatusTypEnum.Over;
            report.CompletedDate = DateTime.Now;
            report.FilePath = filePath;
            repoReport.Update(report);
            using (var trans = _unitOfWork.BeginTransaction())
            {
                try
                {
                    await _unitOfWork.SaveChangesAsync();
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public async Task<List<ReportResultOutput>> GetReport()
        {
            var repoContactInformation = _unitOfWork.Repository<ContactInformation>();
            var resultQueryable = repoContactInformation.GetAll();

            var resultPersonCount = resultQueryable
                     .GroupBy(x => x.Location)
                     .Select(x => new { Location = x.Key, PersonCount = x.Count() }).ToList();


            var resultForPhone = resultQueryable
                .Where(x => x.Phone != null)
                     .GroupBy(x => x.Location)
                     .Select(x => new { Location = x.Key, PhoneCount  = x.Count() }).ToList();


            var result = from person in resultPersonCount
                         join phone in resultForPhone
            on person.Location equals phone.Location // join on some property
            select new ReportResultOutput { Location= person.Location, PhoneCount = phone.PhoneCount, PersonCount = person.PersonCount };
            
            return result.ToList();
        }

        public async Task RequestReport()
        {
            var repo = _unitOfWork.Repository<Report>();

            var newItem = new Report()
            {
                RequestedDate = DateTime.Now,
                ReportStatus = Contracts.Types.Enums.ReportStatusTypEnum.Ongoing
            };
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    repo.Insert(newItem);
                    await _unitOfWork.SaveChangesAsync();
                    _rabbitmqPost.Post(new RabbitmqQueueModel
                    {
                        ChannelName = "Report",
                        QueueObjectArguements = GetQueueObjectArguements(newItem.Id)
                    });
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        #region Privates
        private IDictionary<string, object> GetQueueObjectArguements(long reportId)
        {
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("reportId", reportId);

            return keyValuePairs;
        }
        #endregion
    }
}
