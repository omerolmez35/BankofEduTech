using BankofEduTech.Core.Application.Features.Commands.UserMoney.SendMoney;
using BankofEduTech.Core.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Commands.UserCredit.UserCreditPay
{
    public class UserCreditPayCommandHandler : IRequestHandler<UserCreditPayCommandRequest, UserCreditPayCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly ILogger<UserCreditPayCommandHandler> _logger;
        public UserCreditPayCommandHandler(IUnitOfWork unitOfWork, IMediator mediator, ILogger<UserCreditPayCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<UserCreditPayCommandResponse> Handle(UserCreditPayCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var payment = await _unitOfWork.CustomerCreditInstallementsReadRepository.GetSingleAsync(x => x.InstallementsID == request.InstallementsID);
                var result = await _mediator.Send(new SendMoneyCommandRequest { SenderID = request.SenderID, Amount = Convert.ToDecimal(payment.AmountOfInstallements) + request.DelayInterest, ReceiverID = 1, Description = "Kredi ödemesi" });
                if(result.IsSuccess)
                {
                    payment.IsPaid = true;
                    payment.PaymentDate = DateTime.Now;
                    _unitOfWork.CustomerCreditInstallementsWriteRepository.Update(payment);
                    return new UserCreditPayCommandResponse { IsSuccess = true, Message = "Ödeme işlemi başarılı." };
                }
                else return new UserCreditPayCommandResponse { IsSuccess = false, Message = result.Message };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hata oluştu.");
                return new UserCreditPayCommandResponse { IsSuccess = false, Message = "Ödeme işlemi başarısız!" };
                
            }

         
        }
    }
}
