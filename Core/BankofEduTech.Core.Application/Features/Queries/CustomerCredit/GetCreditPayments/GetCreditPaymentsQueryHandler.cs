using AutoMapper;
using BankofEduTech.Core.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetCreditPayments
{
    public class GetCreditPaymentsQueryHandler : IRequestHandler<GetCreditPaymentsQueryRequest, List<GetCreditPaymentsQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCreditPaymentsQueryHandler> _logger;
        public GetCreditPaymentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetCreditPaymentsQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<GetCreditPaymentsQueryResponse>> Handle(GetCreditPaymentsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var account = await _unitOfWork.CustomerAccountReadRepository.Table.Include(x => x.CustomerCredit).FirstOrDefaultAsync(x => x.ID == request.AccountID);
                var query = await _unitOfWork.CustomerCreditInstallementsReadRepository.GetWhere(x => x.CustomerCreditID == account.CustomerCredit[0].CreditID).ToListAsync();
                var result =  _mapper.Map<List<GetCreditPaymentsQueryResponse>>(query);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hata oluştu.");
                throw;
            }
            
        }
    }
}
