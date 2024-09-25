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

namespace BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetApplicationByUserId
{
    public class GetApplicationByUserIdQueryHandler : IRequestHandler<GetApplicationByUserIdQueryRequest, List<GetApplicationByUserIdQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetApplicationByUserIdQueryHandler> _logger;

        public GetApplicationByUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetApplicationByUserIdQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<GetApplicationByUserIdQueryResponse>> Handle(GetApplicationByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _unitOfWork.CustomerCreditReadRepository.GetWhere(x => x.UserID == request.UserID).ToListAsync();
                var result = _mapper.Map<List<GetApplicationByUserIdQueryResponse>>(data);
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
