using BankofEduTech.Core.Application.Repositories;
using BankofEduTech.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uc = BankofEduTech.Core.Application.StaticServices.BankofEduTech.Core.Application.StaticServices.ClaimService;
namespace BankofEduTech.Core.Application.Features.Queries.UserAccount.GetAllUserAccountActiveUser
{
    public class GetAllAccountQueryHandler : IRequestHandler<GetAllAccountQueryRequest, List<GetAllAccountQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Uc _claimService;
        private readonly ILogger<GetAllAccountQueryHandler> _logger;
        public GetAllAccountQueryHandler(IUnitOfWork unitOfWork, Uc claimService, ILogger<GetAllAccountQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _claimService = claimService;
            _logger = logger;
        }

        public async Task<List<GetAllAccountQueryResponse>> Handle(GetAllAccountQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var values = await _unitOfWork.CustomerAccountReadRepository.Table.Include(x => x.AppUser).Where(x => x.AppUser.Id == request.UserID && x.AccountType == request.AccountType).Select(x => new GetAllAccountQueryResponse
                {
                    AccountName = x.AccountName,
                    AccountBalance = x.Currency == Currency.TRY ? $"{x.Balance} ₺" : x.Currency == Currency.USD ? $"{x.Balance} $" : $"{x.Balance} €",
                    AccountCurrency = x.Currency == Currency.TRY ? "TRY" : x.Currency == Currency.USD ? "USD" : "EUR",
                    AccountType = x.AccountType == AccountType.Vadeli ? "Vadeli" : "Vadesiz",
                    AccountID = x.ID,
                    IsSuccess = true,
                    Message = "İşlem Başarılı",
                    AccountNumber = x.AccountNumber,
                    CreatedDate = x.CreatedDate

                }).ToListAsync();
                if (values.Count > 0) return values;
                return new List<GetAllAccountQueryResponse> { new GetAllAccountQueryResponse { IsSuccess = false, Message = "Hesap Bulunamadı." } };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hata oluştu.");
                throw;
            }
            
        }
    }
}
