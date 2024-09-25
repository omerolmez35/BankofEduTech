using AutoMapper;
using BankofEduTech.Core.Application.Repositories;
using BankofEduTech.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetCustomerCreditApplication
{
    public class GetCustomerCreditApplicationQueryHandler : IRequestHandler<GetCustomerCreditApplicationQueryRequest, List<GetCustomerCreditApplicationQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCustomerCreditApplicationQueryHandler> _logger;

        public GetCustomerCreditApplicationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetCustomerCreditApplicationQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<List<GetCustomerCreditApplicationQueryResponse>> Handle(GetCustomerCreditApplicationQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var data = _unitOfWork.CustomerCreditReadRepository.Table.Include(x => x.AppUser).Where(x => x.CreditStatus == CreditStatus.Beklemede && x.IsActive).ToList();
                var result = _mapper.Map<List<GetCustomerCreditApplicationQueryResponse>>(data);
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hata oluştu.");
                throw;
            }
            
        }
    }
}
