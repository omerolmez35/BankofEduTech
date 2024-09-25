using AutoMapper;
using BankofEduTech.Core.Application.Features.Commands.UserCredit.CreateUserCredit;
using BankofEduTech.Core.Application.Features.Commands.UserCredit.CreateUserCreditApplication;
using BankofEduTech.Core.Application.Features.Commands.UserCredit.UserCreditPay;
using BankofEduTech.Core.Application.Features.Queries.CustomerCredit.CustomerCreditCalculation;
using BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetApplicationByUserId;
using BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetCreditPayments;
using BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetPayCredit;
using BankofEduTech.Core.Application.Features.Queries.UserAccount.GetAllUserAccountActiveUser;
using BankofEduTech.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankofEduTech.API.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCreditController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserCreditController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreditCalculate")]
        public async Task<IActionResult> CreditCalculate(CustomerCreditCalculationQueryRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("GetAllCreditsAccount")]
        public async Task<IActionResult> GetAllCreditsAccount(Guid userID)
        {
            var result = await _mediator.Send(new GetAllAccountQueryRequest(AccountType.Kredi, userID));
            return Ok(result);
        }

        [HttpPost("CreateCredit")]
        public async Task<IActionResult> CreateCredit(CreateUserCreditCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("CreateCreditApplication")]
        public async Task<IActionResult> CreateCreditApplication(CreateUserCreditApplicationCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("GetCreditPaymentTable")]
        public async Task<IActionResult> GetCreditPaymentTable(int accountID)
        {
            var result = await _mediator.Send(new GetCreditPaymentsQueryRequest(accountID));
            return Ok(result);
        }

        [HttpGet("GetPayCredit")]
        public async Task<IActionResult> GetPayCredit(int installementID, Guid userID)
        {
            var result = await _mediator.Send(new GetPayCreditQueryRequest(installementID, userID));
            return Ok(result);
        }

        [HttpPost("CreditPay")]
        public async Task<IActionResult> CreditPay(UserCreditPayCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("GetCreditApplications")]
        public async Task<IActionResult> GetCreditApplications(Guid userID)
        {
            var result = await _mediator.Send(new GetApplicationByUserIdQueryRequest(userID));
            return Ok(result);
        }
    }
}
