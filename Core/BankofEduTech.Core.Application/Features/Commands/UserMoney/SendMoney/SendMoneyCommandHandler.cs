using BankofEduTech.Core.Application.Repositories;
using BankofEduTech.Core.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Commands.UserMoney.SendMoney
{
    public class SendMoneyCommandHandler : IRequestHandler<SendMoneyCommandRequest, SendMoneyCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SendMoneyCommandHandler> _logger;

        public SendMoneyCommandHandler(IUnitOfWork unitOfWork, ILogger<SendMoneyCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<SendMoneyCommandResponse> Handle(SendMoneyCommandRequest request, CancellationToken cancellationToken)
        {

            try
            {

                var customerSender = await _unitOfWork.CustomerAccountReadRepository.GetSingleAsync(x => x.ID == request.SenderID);
                var customerReceiver = await _unitOfWork.CustomerAccountReadRepository.GetSingleAsync(x => x.ID == request.ReceiverID);

                Guid referenceId = Guid.NewGuid();
                if (request.Amount > customerSender.Balance)
                    return new SendMoneyCommandResponse { IsSuccess = false, Message = "Bu işlemi yapmak için bakiyeniz yetersiz!" };
                else
                {
                    customerSender.CustomerAccountActivities = new List<CustomerAccountActivities>();
                    customerSender.CustomerAccountActivities.Add(new CustomerAccountActivities()
                    {

                        Amount = request.Amount,
                        CreatedDate = DateTime.Now,
                        ActivityType = ActivityType.Borc,
                        IsActive = true,
                        IsDeleted = false,
                        Description = request.Description,
                        ReferenceID = referenceId

                    });
                    customerSender.Balance -= request.Amount;

                    customerReceiver.CustomerAccountActivities = new List<CustomerAccountActivities>();
                    customerReceiver.CustomerAccountActivities.Add(new CustomerAccountActivities()
                    {

                        Amount = request.Amount,
                        CreatedDate = DateTime.Now,
                        ActivityType = ActivityType.Alacak,
                        IsActive = true,
                        IsDeleted = false,
                        Description = request.Description,
                        ReferenceID = referenceId


                    });
                    customerReceiver.Balance += request.Amount;

                    _unitOfWork.CustomerAccountWriteRepository.Update(customerSender);
                    _unitOfWork.CustomerAccountWriteRepository.Update(customerReceiver);
                }

                return new SendMoneyCommandResponse() { IsSuccess = true, Message = "Transfer İşlemi Başarılı", ReceiverID = customerReceiver.AppUserID, SenderID = customerSender.AppUserID };


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hata oluştu.");
                return new SendMoneyCommandResponse() { IsSuccess = false, Message = ex.Message };
            }



        }
    }
}
