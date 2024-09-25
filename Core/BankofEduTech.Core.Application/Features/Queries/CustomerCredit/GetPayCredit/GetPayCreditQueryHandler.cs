using BankofEduTech.Core.Application.Features.Queries.UserMoney.GetAllAccountForByUserId;
using BankofEduTech.Core.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uc = BankofEduTech.Core.Application.StaticServices.BankofEduTech.Core.Application.StaticServices.ClaimService;

namespace BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetPayCredit
{
    public class GetPayCreditQueryHandler : IRequestHandler<GetPayCreditQueryRequest, GetPayCreditQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly Uc _claimService;
        private readonly ILogger<GetPayCreditQueryHandler> _logger;
        public GetPayCreditQueryHandler(IUnitOfWork unitOfWork, IMediator mediator, Uc claimService, ILogger<GetPayCreditQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _claimService = claimService;
            _logger = logger;
        }

        public async Task<GetPayCreditQueryResponse> Handle(GetPayCreditQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var value = await _unitOfWork.CustomerCreditInstallementsReadRepository.GetSingleAsync(x => x.InstallementsID == request.InstallementsID);
                var control = _unitOfWork.CustomerCreditInstallementsReadRepository.GetWhere(x => x.CustomerCreditID == value.CustomerCreditID && !x.IsPaid).Min(x => x.NumberOfInstallements);
                if (value.NumberOfInstallements > control) return new GetPayCreditQueryResponse { IsSuccess = false, Message = "Lütfen önce en eski taksidi ödeyiniz." };
                else
                {
                    GetPayCreditQueryResponse result = new GetPayCreditQueryResponse
                    {
                        Amount = Convert.ToDecimal(value.AmountOfInstallements),
                        DelayInterest = value.ExpireDate < DateTime.Now.Date ? Convert.ToDecimal((DateTime.Now.Date - value.ExpireDate).TotalDays * (30 / 4.5)).ToString("N2") : "0",
                        NumberOfInstallements = value.NumberOfInstallements,
                        IsSuccess = true,
                        Message = "İşlem başarılı.",
                        AllAccount = await _mediator.Send(new GetAllAccountForByUserIdQueryRequest { UserID = request.UserID })

                    };
                    return result;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hata oluştu.");
                throw;
            }
        }
    }
}
