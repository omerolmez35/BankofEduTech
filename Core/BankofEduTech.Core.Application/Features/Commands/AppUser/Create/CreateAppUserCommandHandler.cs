using AutoMapper;
using BankofEduTech.Core.Application.Abstractions;
using BankofEduTech.Core.Application.ViewModels;
using BankofEduTech.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Text;


namespace BankofEduTech.Core.Application.Features.Commands.AppUser.Create
{
    public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommandRequest, CreateAppUserCommandResponse>
    {
        private readonly UserManager<Domain.Entities.AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<CreateAppUserCommandHandler> _logger;

        public CreateAppUserCommandHandler(UserManager<Domain.Entities.AppUser> userManager, IMapper mapper, IEmailSender emailSender, ILogger<CreateAppUserCommandHandler> logger, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _emailSender = emailSender;
            _logger = logger;
            _roleManager = roleManager;
        }

        public async Task<CreateAppUserCommandResponse> Handle(CreateAppUserCommandRequest request, CancellationToken cancellationToken)
        {

            try
            {
                var value = _mapper.Map<Domain.Entities.AppUser>(request);
                var result = await _userManager.CreateAsync(value, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(value, "Customer");
                    var token = _userManager.GenerateEmailConfirmationTokenAsync(value).Result;
                    var encToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                    var confirmUrl = $"https://localhost:7098/Login/ActivationMail?u={value.Id}&t={encToken}";

                    await _emailSender.SendMailAsync(new EmailMessageModel()
                    {
                        Subject = $"BANK OF EDUTECH - HOŞGELDİNİZ - AKTİVASYON İŞLEMİ",
                        To = value.Email,
                        Body = $"<b>Merhaba {value.Name} {value.Surname}, </b><br/>" +
                             $"<h4>Sisteme kaydınız başarılıdır!</h4><br/>" +
                             $"Sistemi kullanabilmeniz için aktivasyon yapmanız gerekiyor. <a href='{confirmUrl}' target='_blank'>Buraya</a> tıklayarak aktivasyonu yapabilirsiniz."
                    });
                    return new CreateAppUserCommandResponse() { IsSuccess = true, Messages = new List<string> { "Kullanıcı başarıyla eklendi." } };
                }
                return new CreateAppUserCommandResponse() { IsSuccess = false, Messages = result.Errors.Select(e => e.Description).ToList() };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hata oluştu.");
                throw;
            }

        }
    }
}
