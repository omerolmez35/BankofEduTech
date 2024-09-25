using BankofEduTech.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Commands.UserCredit.CreateUserCredit
{
    public class CreateUserCreditCommandRequest : IRequest<CreateUserCreditCommandResponse>
    {
        //public double AmountOfLoan { get; set; }
        //public double InterestRate { get; set; }
        //public int NumberofInstallement { get; set; }
        public int CreditID { get; set; }
        public CreditStatus CreditStatus { get; set; }
        //public Guid UserID { get; set; }

    }
}
