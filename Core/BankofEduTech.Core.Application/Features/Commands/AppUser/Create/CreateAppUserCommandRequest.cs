using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Commands.AppUser.Create
{
	public class CreateAppUserCommandRequest : IRequest<CreateAppUserCommandResponse>
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public string City { get; set; }
		public string District { get; set; }
		public string ImageUrl { get; set; }
		public string IdentityNumber { get; set; }
	}
}
