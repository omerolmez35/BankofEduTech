using BankofEduTech.Core.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uc = BankofEduTech.Core.Application.StaticServices.BankofEduTech.Core.Application.StaticServices.ClaimService;

namespace BankofEduTech.Core.Application.Features.Queries.AppUser.GetUserInfo
{
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQueryRequest, GetUserInfoQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Uc _claimService;
        private readonly ILogger<GetUserInfoQueryHandler> _logger;

        public GetUserInfoQueryHandler(IUnitOfWork unitOfWork, Uc claimService, ILogger<GetUserInfoQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _claimService = claimService;
            _logger = logger;
        }

        public async Task<GetUserInfoQueryResponse> Handle(GetUserInfoQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                GetUserInfoQueryResponse response = new();
                var accounts = await _unitOfWork.CustomerAccountReadRepository.Table.Include(x => x.CustomerCredit).Where(x => x.IsActive && x.AppUserID == request.UserID).ToListAsync();

                response.TotalAccount = accounts.Where(x => x.AccountType == Domain.Entities.AccountType.Vadesiz).Count();
                response.TotalBalance = accounts.Where(x => x.AccountType == Domain.Entities.AccountType.Vadesiz).Sum(x => x.Balance);
                foreach (var account in accounts.Where(x => x.AccountType == Domain.Entities.AccountType.Kredi))
                {
                    foreach (var item in account.CustomerCredit)
                    {
                        response.TotalDebt += item.TotalBackPaymnent;
                        response.LatePayment += _unitOfWork.CustomerCreditInstallementsReadRepository.GetWhere(x => !x.IsPaid && x.ExpireDate < DateTime.Now.Date && x.CustomerCreditID == item.CreditID).Count();

                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hata oluştu.");
                return new GetUserInfoQueryResponse();
             
            }
        
           
        }
    }
}
