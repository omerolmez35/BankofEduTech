using AutoMapper;
using BankofEduTech.Core.Application.Features.Commands.UserAccount.Create;
using BankofEduTech.Core.Application.Features.Commands.UserMoney.SendMoney;
using BankofEduTech.Core.Application.Features.Queries.CustomerCredit.CustomerCreditCalculation;
using BankofEduTech.Core.Application.Repositories;
using BankofEduTech.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Uc = BankofEduTech.Core.Application.StaticServices.BankofEduTech.Core.Application.StaticServices.ClaimService;

namespace BankofEduTech.Core.Application.Features.Commands.UserCredit.CreateUserCredit
{
    public class CreateUserCreditCommandHandler : IRequestHandler<CreateUserCreditCommandRequest, CreateUserCreditCommandResponse>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Uc _claimService;
        private readonly ILogger<CreateUserCreditCommandHandler> _logger;




        public CreateUserCreditCommandHandler(IMediator mediator, IMapper mapper, IUnitOfWork unitOfWork, Uc claimService, ILogger<CreateUserCreditCommandHandler> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _claimService = claimService;
            _logger = logger;
        }

        public async Task<CreateUserCreditCommandResponse> Handle(CreateUserCreditCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {

                var creditApp = await _unitOfWork.CustomerCreditReadRepository.GetSingleAsync(x => x.CreditID == request.CreditID);
                if (request.CreditStatus == CreditStatus.Onay)
                {
                    var result = await _mediator.Send(new CustomerCreditCalculationQueryRequest(Convert.ToDouble(creditApp.AmountOfLoan), Convert.ToDouble(creditApp.InterestRate), creditApp.CountofInstallements));
                    var mapData = _mapper.Map<List<CustomerCreditInstallements>>(result);
         
                    var accountResult = await _mediator.Send(new CreateUserAccountCommandRequest { AccountName = "KREDİ HESABIM", AccountType = (int)AccountType.Kredi, Currency = (int)Currency.TRY, AccountID = creditApp.UserID });
                    foreach (var item in mapData)
                    {
                        item.IsActive = true;
                        item.IsPaid = false;
                        item.CreatedDate = DateTime.Now;
                        item.IsDeleted = false;
                        item.CustomerCreditID = creditApp.CreditID;

                    }

                    _unitOfWork.CustomerCreditInstallementsWriteRepository.Table.AddRange(mapData);
                    creditApp.CreditStatus = CreditStatus.Onay;
                    creditApp.CustomerAccountID = accountResult.AccountID;
                    _unitOfWork.CustomerCreditWriteRepository.Update(creditApp);
                    await _mediator.Send(new SendMoneyCommandRequest { Amount = Convert.ToDecimal(creditApp.AmountOfLoan), ReceiverID = accountResult.AccountID, SenderID = 1, Description = "Kredi Kullandırma İşlemi" });
                    await _mediator.Send(new SendMoneyCommandRequest { Amount = Convert.ToDecimal(creditApp.AmountOfLoan), ReceiverID = creditApp.PaymentAccountID, SenderID = 1, Description = "Kredi kullanım işlemi" });
    
                    return new CreateUserCreditCommandResponse { IsSuccess = true, Message = "Kredi başarıyla oluşturuldu ve hesaba aktarıldı.", UserID = creditApp.UserID, CreditStatus = CreditStatus.Onay };
                }
                else if (request.CreditStatus == CreditStatus.Red)
                {
                    creditApp.CreditStatus = CreditStatus.Red;
                    _unitOfWork.CustomerCreditWriteRepository.Update(creditApp);
                    return new CreateUserCreditCommandResponse { IsSuccess = true, Message = "Kredi başarıyla reddedildi.", UserID = creditApp.UserID, CreditStatus = CreditStatus.Red };
                }


                return new CreateUserCreditCommandResponse { IsSuccess = false, Message = "Hata." };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "HAta oluştu.");
                throw;
            }


        }
    }
}
