using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using BankofEduTech.Core.Domain.Entities;
using System.ComponentModel.Design;
using Microsoft.Extensions.Logging;

namespace BankofEduTech.Core.Application.Features.Queries.AppUser.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQueryRequest, LoginUserQueryResponse>
    {
        private readonly SignInManager<Domain.Entities.AppUser> _signInManager;
        private readonly UserManager<Domain.Entities.AppUser> _userManager;
        private readonly ILogger<LoginUserQueryHandler> _logger;

        public LoginUserQueryHandler(SignInManager<Domain.Entities.AppUser> signInManager, UserManager<Domain.Entities.AppUser> userManager, ILogger<LoginUserQueryHandler> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<LoginUserQueryResponse> Handle(LoginUserQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, true);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(request.Username);
                    var roles = await _userManager.GetRolesAsync(user);
                    if (user != null)
                    {
                        if (user.EmailConfirmed == true) return new LoginUserQueryResponse() { IsSuccess = true, Message = "Giriş işlemi başarılı.", Name = user.Name, Surname = user.Surname, Username = user.UserName, UserID = user.Id.ToString(), Roles = roles };
                        else return new LoginUserQueryResponse() { IsSuccess = false, Message = "Email adresiniz henüz doğrulanmamış. Lütfen mailde gelen linke tıklayarak hesabınızı doğrulayınız." };
                    }
                    else return new LoginUserQueryResponse() { IsSuccess = false, Message = "Kullanıcı bulunamadı." };
                }
                else return new LoginUserQueryResponse() { IsSuccess = false, Message = "Giriş işlemi başarısız." };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hata oluştu.");
                return new LoginUserQueryResponse() { IsSuccess = false, Message = "Giriş işlemi başarısız." };
            }
           
        }



    }
}
