using AutoMapper;
using BankofEduTech.Core.Application.Features.Commands.UserAccount.Create;
using BankofEduTech.Core.Application.Features.Queries.CustomerCredit.CustomerCreditCalculation;
using BankofEduTech.Core.Application.Repositories;
using BankofEduTech.Core.Application.StaticServices.BankofEduTech.Core.Application.StaticServices;
using BankofEduTech.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Commands.UserCredit.CreateUserCreditApplication
{
    public class CreateUserCreditApplicationCommandHandler : IRequestHandler<CreateUserCreditApplicationCommandRequest, CreateUserCreditApplicationCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly ClaimService _claimService;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUserCreditApplicationCommandHandler> _logger;

        public CreateUserCreditApplicationCommandHandler(IUnitOfWork unitOfWork, IMediator mediator, ClaimService claimService, IMapper mapper, ILogger<CreateUserCreditApplicationCommandHandler> logger)
        {
            _mediator = mediator;
            _claimService = claimService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CreateUserCreditApplicationCommandResponse> Handle(CreateUserCreditApplicationCommandRequest request, CancellationToken cancellationToken)
        {

            try
            {
                var control = await _unitOfWork.CustomerCreditReadRepository.GetSingleAsync(x => x.UserID == request.UserID && x.CreditStatus == CreditStatus.Beklemede);
                if (control == null)
                {
                    var result = await _mediator.Send(new CustomerCreditCalculationQueryRequest(request.AmountOfLoan, request.InterestRate, request.NumberofInstallement));
                    var mapData = _mapper.Map<List<CustomerCreditInstallements>>(result);


                    CustomerCredits customerCredits = new CustomerCredits
                    {

                        AmountOfInstallement = Convert.ToDecimal(mapData[0].AmountOfInstallements),
                        IsActive = true,
                        NumberofInstallement = mapData[0].NumberOfInstallements,
                        IsDeleted = false,
                        CreatedDate = DateTime.Now,
                        LastInstallementDate = mapData.OrderByDescending(x => x.NumberOfInstallements).FirstOrDefault().ExpireDate,
                        AmountOfLoan = (decimal)request.AmountOfLoan,
                        TotalBackPaymnent = result.Sum(x => Convert.ToDecimal(x.AmountOfInstallements)),
                        CreditStatus = CreditStatus.Beklemede,
                        InterestRate = (decimal)request.InterestRate,
                        PaymentAccountID = request.PaymentAccountID,
                        UserID = request.UserID,
                        CountofInstallements = mapData.Count(),
                        

                    };
                    await _unitOfWork.CustomerCreditWriteRepository.AddAsync(customerCredits);
                    return new CreateUserCreditApplicationCommandResponse { IsSuccess = true, Message = "Kredi başvurunuz başarıyla oluşturuldu. Başvuru durumunuzu Kredi İşlemleri/Başvuru Takip menüsünden takip edebilirsiniz." };
                }
                else return new CreateUserCreditApplicationCommandResponse { IsSuccess = false, Message = "Henüz sonuçlanmamış kredi başvurunuz bulunmaktadır." };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "HAta oluştu.");
                throw;
            }

        }
    }
}
