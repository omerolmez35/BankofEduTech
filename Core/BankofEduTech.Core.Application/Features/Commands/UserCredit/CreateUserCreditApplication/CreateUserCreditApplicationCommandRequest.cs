using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Commands.UserCredit.CreateUserCreditApplication
{
    public class CreateUserCreditApplicationCommandRequest : IRequest<CreateUserCreditApplicationCommandResponse>
    {
        public double AmountOfLoan { get; set; }
        public double InterestRate { get; set; }
        public int NumberofInstallement { get; set; }
        public Guid UserID { get; set; }
        public int PaymentAccountID { get; set; }

  
    }
}
