using BankofEduTech.Core.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.UserMoney.GetAccountInfo
{
    public class GetAccountInfoQueryHandler : IRequestHandler<GetAccountInfoQueryRequest, GetAccountInfoQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAccountInfoQueryHandler> _logger;

        public GetAccountInfoQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAccountInfoQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<GetAccountInfoQueryResponse> Handle(GetAccountInfoQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {

                var user = await _unitOfWork.CustomerAccountReadRepository.Table.Include(x => x.AppUser).FirstOrDefaultAsync(x => x.AccountNumber == request.AccountNumber);
                var compare = await _unitOfWork.CustomerAccountReadRepository.Table.Include(x => x.AppUser).FirstOrDefaultAsync(x => x.ID == request.SenderAccountNumberID);

                if (user != null)
                {
                    if (user.AppUserID != compare.AppUserID)
                    {
                        if (compare.Currency != user.Currency) return new GetAccountInfoQueryResponse() { IsSuccess = false, Message = "Gönderici ve Alıcı Hesabın Para Birimi Aynı Olmalı!" };
                        else
                            return new GetAccountInfoQueryResponse() { IsSuccess = true, Message = "İşlem Başarılı", Name = user.AppUser.Name, Surname = user.AppUser.Surname, AccountID = user.ID };
                    }
                    else return new GetAccountInfoQueryResponse() { IsSuccess = false, Message = "Alıcı hesap farklı bir kişi olmalıdır!!" };
                }


                else
                    return new GetAccountInfoQueryResponse() { IsSuccess = false, Message = "Bu hesap numarasına ait bir hesap bulunamadı!" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hata oluştu.");
                return new GetAccountInfoQueryResponse() { IsSuccess = false, Message = ex.Message };

            }


        }
    }
}
