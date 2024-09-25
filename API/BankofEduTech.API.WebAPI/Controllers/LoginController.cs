using BankofEduTech.Core.Application.Abstractions;
using BankofEduTech.Core.Application.Features.Commands.AppUser.Create;
using BankofEduTech.Core.Application.Features.Queries.AppUser.LoginUser;
using BankofEduTech.Core.Application.ViewModels;
using BankofEduTech.Core.Domain.Entities;
using BankofEduTech.Infrastructure.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace BankofEduTech.API.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IEmailSender _emailSender;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;

        public LoginController(IMediator mediator, IEmailSender emailSender, UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _mediator = mediator;
            _emailSender = emailSender;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateAppUserCommandRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(request);


                return Ok(result);


            }

            return Ok();
        }


        [HttpPost("ActivationMail")]
        public async Task<IActionResult> ActivationMail(EmailActivationModel model)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(model.u);
                if (user.EmailConfirmed)
                {
                    return Ok(new ResultModel() { IsSuccess = true, Message = "Aktivasyon işlemi başarılı." });
                }
                if (user != null)
                {
                    var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.t));
                    var result = await _userManager.ConfirmEmailAsync(user, token);

                    if (result.Succeeded)
                    {
                        return Ok(new ResultModel() { IsSuccess = true, Message = "Aktivasyon işlemi başarılı." });

                    }
                    else return Ok(new ResultModel() { IsSuccess = false, Message = "Aktivasyon işlemi sırasında hata oluştu." });

                }
                else return Ok(new ResultModel() { IsSuccess = false, Message = "Kullanıcı bulunamadı." });

            }
            catch (Exception)
            {

                return Ok(new ResultModel() { IsSuccess = false, Message = "Aktivasyon işlemi sırasında hata oluştu." });
            }
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(LoginUserQueryRequest request)
        {
            var result = await _mediator.Send(request);
            if (result.IsSuccess)
                result.Token = _tokenService.CreateToken(result);



            return Ok(result);
        }
    }
}
