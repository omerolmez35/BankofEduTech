using BankofEduTech.Core.Application.Features.Commands.AppUser.Update;
using BankofEduTech.Core.Application.Features.Queries.AppUser.GetUserByName;
using BankofEduTech.Core.Domain.Entities;
using BankofEduTech.Infrastructure.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankofEduTech.API.WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
      private readonly BankofEduTechContext _context;

        public AccountController(IMediator mediator, BankofEduTechContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpGet("GetActiveUserByName")]
        public async Task<IActionResult> GetActiveUserByName(Guid userID)
        {
            var result = await _mediator.Send(new GetUserByNameQueryRequest(userID));
            return Ok(result);
        }

        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UpdateAppUserCommandRequest request)
        {
         //   _context.CustomerAccounts.Where

            request.Username = User.Identity.Name;
            var result = await _mediator.Send(request);
            return Ok(result);
        }

     

   
    }
}
