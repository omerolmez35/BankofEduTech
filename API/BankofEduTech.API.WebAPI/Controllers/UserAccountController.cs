using BankofEduTech.Core.Application.Features.Commands.UserAccount.Create;
using BankofEduTech.Core.Application.Features.Queries.AppUser.GetUserInfo;
using BankofEduTech.Core.Application.Features.Queries.UserAccount.GetAccountActivities;
using BankofEduTech.Core.Application.Features.Queries.UserAccount.GetAllUserAccountActiveUser;
using BankofEduTech.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankofEduTech.Core.Application.StaticServices.BankofEduTech.Core.Application.StaticServices;

namespace BankofEduTech.API.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ClaimService _claimService;


        public UserAccountController(IMediator mediator, ClaimService claimService)
        {
            _mediator = mediator;
            _claimService = claimService;
        }

        [HttpPost("CreateUserAccount")]
        public async Task<IActionResult> CreateUserAccount(CreateUserAccountCommandRequest request)
        {
            request.AccountType = (int)AccountType.Vadesiz;
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("GetAllAccount")]
        public async Task<IActionResult> GetAllAccount(Guid userID)
        {
            var user = ClaimService.CurrentUser.Surname;
            var result = await _mediator.Send(new GetAllAccountQueryRequest(AccountType.Vadesiz, userID));
            return Ok(result);
        }

        [HttpGet("GetAllCreditAccounts")]
        public async Task<IActionResult> GetAllCreditAccounts(Guid userID)
        {

            var result = await _mediator.Send(new GetAllAccountQueryRequest(AccountType.Kredi, userID));
            return Ok(result);
        }

        [HttpGet("GetAllAccountActivities")]
        public async Task<IActionResult> GetAllAccountActivities(int accountID)
        {
            try
            {
                var result = await _mediator.Send(new GetAccountActivitiesQueryRequest(accountID));
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpGet("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo(Guid userID)
        {
            var result = await _mediator.Send(new GetUserInfoQueryRequest(userID));
            return Ok(result);
        }
    }
}
