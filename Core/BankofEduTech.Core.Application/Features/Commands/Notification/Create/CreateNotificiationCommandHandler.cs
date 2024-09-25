using AutoMapper;
using BankofEduTech.Core.Application.Repositories;
using BankofEduTech.Core.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Commands.Notification.Create
{
    public class CreateNotificiationCommandHandler : IRequestHandler<CreateNotificiationCommandRequest, CreateNotificiationCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateNotificiationCommandHandler> _logger;

        public CreateNotificiationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateNotificiationCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CreateNotificiationCommandResponse> Handle(CreateNotificiationCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var mapData = _mapper.Map<Notifications>(request);
                await _unitOfWork.NotificationWriteRepository.AddAsync(mapData);
                return new CreateNotificiationCommandResponse { IsSuccess = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hata oluştu.");
                throw;
            }
        }
    }
}
