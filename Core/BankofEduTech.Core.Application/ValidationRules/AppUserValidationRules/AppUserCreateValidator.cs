using BankofEduTech.Core.Application.Features.Commands.AppUser.Create;
using BankofEduTech.Core.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.ValidationRules.AppUserValidationRules
{
	public class AppUserCreateValidator : AbstractValidator<CreateAppUserCommandRequest>
	{
        public AppUserCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad alanı boş geçilemez.");
            RuleFor(x => x.Name).MaximumLength(30).WithMessage("Lütfen en fazla 30 karakter giriniz.");
            RuleFor(x => x.Name).MinimumLength(2).WithMessage("Lütfen en az 2 karakter giriniz.");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyad alanı boş geçilemez.");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı adı alanı boş geçilemez.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email alanı boş geçilemez.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email adresini doğru formatta giriniz.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı boş geçilemez.");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Şifre tekrar alanı boş geçilemez.");
            RuleFor(x => x.ConfirmPassword).Equal(y => y.Password).WithMessage("Şifreler uyuşmuyor.");
        }
    }
}
