using BankofEduTech.Core.Application.Repositories;
using BankofEduTech.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.UserAccount.GetAccountActivities
{
    public class GetAccountActivitiesQueryHandler : IRequestHandler<GetAccountActivitiesQueryRequest, List<GetAccountActivitiesQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAccountActivitiesQueryHandler> _logger;

        public GetAccountActivitiesQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAccountActivitiesQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<List<GetAccountActivitiesQueryResponse>> Handle(GetAccountActivitiesQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var activities =  _unitOfWork.CustomerAccountActivitiesReadRepository.GetWhere(x => x.CustomerAccountID == request.AccountID).Select(x => new GetAccountActivitiesQueryResponse { ActivityType = x.ActivityType == ActivityType.Borc ? "Giden" : "Gelen", ActivityDate = x.CreatedDate, Description = x.Description, IsSuccess = true, Message = "İşlem Başarılı", Amount = x.Amount }).ToList();
                if (activities.Count > 0) return activities;

                else
                    return new List<GetAccountActivitiesQueryResponse> { new GetAccountActivitiesQueryResponse { IsSuccess = false, Message = "Hareket Bulunamadı." } };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hata oluştu.");
                throw;
            }
            
        }
    }
}
