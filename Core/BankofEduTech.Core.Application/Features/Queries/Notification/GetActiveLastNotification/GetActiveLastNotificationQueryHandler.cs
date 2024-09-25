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

namespace BankofEduTech.Core.Application.Features.Queries.Notification.GetActiveLastNotification
{
    public class GetActiveLastNotificationQueryHandler : IRequestHandler<GetActiveLastNotificationQueryRequest, List<GetActiveLastNotificationQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetActiveLastNotificationQueryHandler> _logger;

        public GetActiveLastNotificationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetActiveLastNotificationQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<GetActiveLastNotificationQueryResponse>> Handle(GetActiveLastNotificationQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var notification = await _unitOfWork.NotificationReadRepository.GetWhere(x => x.IsActive && !x.IsDeleted && x.UserID == request.UserID).OrderBy(x => x.IsRead).ThenByDescending(x => x.CreatedDate).ToListAsync();
                var not = _mapper.Map<List<GetActiveLastNotificationQueryResponse>>(notification);
                return not;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Hata oluştu.");
                throw;
            }
            

        }
    }
}
