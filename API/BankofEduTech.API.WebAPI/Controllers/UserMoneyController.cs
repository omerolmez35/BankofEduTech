using BankofEduTech.Core.Application.Features.Commands.UserMoney.SendMoney;
using BankofEduTech.Core.Application.Features.Queries.UserMoney.GetAccountInfo;
using BankofEduTech.Core.Application.Features.Queries.UserMoney.GetAllAccountForByUserId;
using BankofEduTech.Core.Application.StaticServices.BankofEduTech.Core.Application.StaticServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankofEduTech.API.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMoneyController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ClaimService _claimService;

        public UserMoneyController(IMediator mediator, ClaimService claimService)
        {
            _mediator = mediator;
            _claimService = claimService;
        }

        [HttpPost("SendMoney")]
        public async Task<IActionResult> SendMoney(SendMoneyCommandRequest request)
        {
            
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("GetAccountInfo")]
        public async Task<IActionResult> GetAccountInfo(GetAccountInfoQueryRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
        [HttpPost("GetAllAccountForByUserId")]
        public async Task<IActionResult> GetAllAccountForByUserId(GetAllAccountForByUserIdQueryRequest request)
        {
            
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
