using AutoMapper;
using BankofEduTech.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uc = BankofEduTech.Core.Application.StaticServices.BankofEduTech.Core.Application.StaticServices.ClaimService;
namespace BankofEduTech.Core.Application.Features.Queries.AppUser.GetUserByName
{
    public class GetUserByNameQueryHandler : IRequestHandler<GetUserByNameQueryRequest, GetUserByNameQueryResponse>
    {
        private readonly UserManager<Domain.Entities.AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly Uc _claimService;
        private readonly ILogger<GetUserByNameQueryHandler> _logger;
        public GetUserByNameQueryHandler(UserManager<Domain.Entities.AppUser> userManager, IMapper mapper, Uc claimService, ILogger<GetUserByNameQueryHandler> logger)
        {
            _userManager = userManager;
            _mapper = mapper;
            _claimService = claimService;
            _logger = logger;
        }

        public async Task<GetUserByNameQueryResponse> Handle(GetUserByNameQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var value = await _userManager.FindByIdAsync(request.UserID.ToString());
                if (value != null)
                {
                    var result = _mapper.Map<GetUserByNameQueryResponse>(value);
                    result.IsSuccess = true;
                    result.Message = "İşlem başarılı";
                    return result;
                }
                return new GetUserByNameQueryResponse() { IsSuccess = false, Message = "Kullanıcı bulunamadı." };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hata oluştu.");
                throw;
            }
           
            
        }
    }
}
