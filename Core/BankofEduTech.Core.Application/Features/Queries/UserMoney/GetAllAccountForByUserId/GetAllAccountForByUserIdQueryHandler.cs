using BankofEduTech.Core.Application.Repositories;
using BankofEduTech.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.UserMoney.GetAllAccountForByUserId
{
    public class GetAllAccountForByUserIdQueryHandler : IRequestHandler<GetAllAccountForByUserIdQueryRequest, List<GetAllAccountForByUserIdQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Domain.Entities.AppUser> _userManager;
        private readonly ILogger<GetAllAccountForByUserIdQueryHandler> _logger;
        public GetAllAccountForByUserIdQueryHandler(IUnitOfWork unitOfWork, UserManager<Domain.Entities.AppUser> userManager, ILogger<GetAllAccountForByUserIdQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<List<GetAllAccountForByUserIdQueryResponse>> Handle(GetAllAccountForByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            Guid userID = Guid.Empty; 
            try
            {
                Domain.Entities.AppUser user = null;
                if (!string.IsNullOrEmpty(request.UserName))
                {
                    user = await _userManager.FindByNameAsync(request.UserName);
                    if (user != null)
                    {
                        userID = user.Id;
                    }
                }
                else if (request.UserID != Guid.Empty)
                {
                    userID = request.UserID ?? Guid.Empty; 
                }

                if (userID != Guid.Empty)
                {
                    var values = await _unitOfWork.CustomerAccountReadRepository
                        .GetWhere(x => x.AppUserID == userID && x.AccountType == AccountType.Vadesiz && x.IsActive && x.Currency == Currency.TRY)
                        .Select(x => new GetAllAccountForByUserIdQueryResponse
                        {
                            IsSuccess = true,
                            Message = "İşlem Başarılı",
                            AccountNumber = x.AccountNumber,
                            Balance = x.Balance,
                            AccountName = x.AccountName,
                            AccountID = x.ID
                        })
                        .ToListAsync();
                    return values;
                }

                return new List<GetAllAccountForByUserIdQueryResponse>
        {
            new GetAllAccountForByUserIdQueryResponse
            {
                IsSuccess = false,
                Message = "Kullanıcı bulunamadı!"
            }
        };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hata oluştu");
                return new List<GetAllAccountForByUserIdQueryResponse>
        {
            new GetAllAccountForByUserIdQueryResponse
            {
                Message = ex.Message,
                IsSuccess = false
            }
        };
            }
        }
    }
}
