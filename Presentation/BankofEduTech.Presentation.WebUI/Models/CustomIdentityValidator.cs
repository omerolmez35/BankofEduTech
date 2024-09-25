using Microsoft.AspNetCore.Identity;
namespace BankofEduTech.Presentation.WebUI.Models
{
	public class CustomIdentityValidator : IdentityErrorDescriber
	{
		public override IdentityError PasswordTooShort(int length)
		{
			return new IdentityError()
			{ 
				Code = "PasswordTooShort",
				Description = $"Parola en az {length} karakter olmalıdır."
			};
		}

		public override IdentityError DuplicateUserName(string userName)
		{
			return new IdentityError()
			{
				Code = "DuplicateUserName",
				Description = $"'{userName}' kullanıcı adı daha önceden alınmış."
			};
		}
		public override IdentityError PasswordRequiresUpper()
		{
			return new IdentityError()
			{
				Code = "PasswordRequiresUpper",
				Description = "Lütfen en az 1 büyük harf girin."
			};
		}


		public override IdentityError PasswordRequiresLower()
		{
			return new IdentityError()
			{
				Code = "PasswordRequiresLower",
				Description = "Lütfen en az 1 küçük harf girin."
			};
		}


		public override IdentityError PasswordRequiresNonAlphanumeric()
		{
			return new IdentityError()
			{
				Code = "PasswordRequiresNonAlphanumeric",
				Description = "Lütfen en az 1 özel karakter girin."
			};
		}

		public override IdentityError InvalidEmail(string email)
		{
			return new IdentityError()
			{
				Code = "InvalidEmail",
				Description = "Lütfen e-mail adresinizi uygun formatta girin."
			};
		}
	}
}
