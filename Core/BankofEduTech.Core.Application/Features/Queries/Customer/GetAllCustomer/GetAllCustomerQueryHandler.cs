using AutoMapper;
using BankofEduTech.Core.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.Customer.GetAllCustomer
{
    public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQueryRequest, List<GetAllCustomerQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllCustomerQueryHandler> _logger;

        public GetAllCustomerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllCustomerQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<List<GetAllCustomerQueryResponse>> Handle(GetAllCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var list = _unitOfWork.AppUserReadRepository.GetAll();
                var customerList = _mapper.Map<List<GetAllCustomerQueryResponse>>(list);
                return Task.FromResult(customerList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hata oluştu.");
                throw;
            }
            
        }
    }
}
