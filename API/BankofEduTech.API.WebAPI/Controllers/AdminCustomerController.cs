using BankofEduTech.Core.Application.Features.Queries.AppUser.GetUserByName;
using BankofEduTech.Core.Application.Features.Queries.Customer.GetAllCustomer;
using BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetApplicationByUserId;
using BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetCustomerCreditApplication;
using BankofEduTech.Core.Application.Features.Queries.UserAccount.GetAllUserAccountActiveUser;
using BankofEduTech.Core.Application.Features.Queries.UserMoney.GetAccountInfo;
using BankofEduTech.Core.Application.ViewModels;
using BankofEduTech.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankofEduTech.API.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminCustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminCustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllCustomer")]
        public async Task<IActionResult> GetAllCustomer()
        {
            var result = await _mediator.Send(new GetAllCustomerQueryRequest());
            return Ok(result);
        }

        [HttpGet("CustomerDetail")]
        public async Task<IActionResult> CustomerDetail(Guid customerID)
        {
            CustomerDetailViewModel customerDetail = new CustomerDetailViewModel();
            customerDetail.AllAccount = await _mediator.Send(new GetAllAccountQueryRequest(AccountType.Vadesiz, customerID));
            customerDetail.AllCreditAccount = await _mediator.Send(new GetAllAccountQueryRequest(AccountType.Kredi, customerID));
            customerDetail.AccountInfo = await _mediator.Send(new GetUserByNameQueryRequest(customerID));
            customerDetail.AllApplication = await _mediator.Send(new GetApplicationByUserIdQueryRequest(customerID));
    
            return Ok(customerDetail);
        }

        [HttpGet("GetCustomerCreditApplication")]
        public async Task<IActionResult> GetCustomerCreditApplication()
        {
            var result = await _mediator.Send(new GetCustomerCreditApplicationQueryRequest());

            return Ok(result);
        }
    }
}
