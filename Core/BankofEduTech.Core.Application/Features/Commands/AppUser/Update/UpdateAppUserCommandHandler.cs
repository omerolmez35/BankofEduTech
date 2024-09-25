using BankofEduTech.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Commands.AppUser.Update
{
    public class UpdateAppUserCommandHandler : IRequestHandler<UpdateAppUserCommandRequest, UpdateAppUserCommandResponse>
    {
        private readonly UserManager<Domain.Entities.AppUser> _userManager;
        private readonly ILogger<UpdateAppUserCommandHandler> _logger;

        public UpdateAppUserCommandHandler(UserManager<Domain.Entities.AppUser> userManager, ILogger<UpdateAppUserCommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<UpdateAppUserCommandResponse> Handle(UpdateAppUserCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.Username);

                if (user != null)
                {
                    if (!string.IsNullOrEmpty(request.Password))
                    {
                        var passwordValidationResult = await _userManager.PasswordValidators.First().ValidateAsync(_userManager, user, request.Password);
                        if (passwordValidationResult.Succeeded)
                        {
                            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.Password);
                        }
                        else
                        {
                            return new UpdateAppUserCommandResponse() { IsSuccess = false, Messages = passwordValidationResult.Errors.Select(x => x.Description).ToList() };
                        }
                    }

                    user.Name = request.Name;
                    user.Surname = request.Surname;
                    user.Email = request.Email;
                    user.City = request.City;
                    user.District = request.District;
                    user.PhoneNumber = request.PhoneNumber;
                    user.ImageUrl = "test"; 

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return new UpdateAppUserCommandResponse() { IsSuccess = true, Messages = new List<string> { "İşlem Başarılı." } };
                    }
                    else
                    {
                        return new UpdateAppUserCommandResponse() { IsSuccess = false, Messages = new List<string> { "İşlem Başarısız!" } };
                    }
                }
                else
                {
                    return new UpdateAppUserCommandResponse { IsSuccess = false, Messages = { "Kullanıcı bulunamadı." } };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hata oluştu.");
                throw;
            }

        }

    }
}
