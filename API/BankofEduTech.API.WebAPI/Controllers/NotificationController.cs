using BankofEduTech.Core.Application.Features.Commands.Notification.Create;
using BankofEduTech.Core.Application.Features.Queries.Notification.GetActiveLastNotification;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankofEduTech.API.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetActiveLastNotification")]
        public async Task<IActionResult> GetActiveLastNotification(Guid userID)
        {
            var result = await _mediator.Send(new GetActiveLastNotificationQueryRequest(userID));
            return Ok(result);
        }

        [HttpPost("CreateNotification")]
        public async Task<IActionResult> CreateNotification(CreateNotificiationCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
