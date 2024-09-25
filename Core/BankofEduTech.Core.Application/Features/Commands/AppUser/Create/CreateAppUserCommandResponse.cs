using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Commands.AppUser.Create
{
	public class CreateAppUserCommandResponse
	{
        public bool IsSuccess { get; set; }

        public List<string> Messages { get; set; }

	}
}
