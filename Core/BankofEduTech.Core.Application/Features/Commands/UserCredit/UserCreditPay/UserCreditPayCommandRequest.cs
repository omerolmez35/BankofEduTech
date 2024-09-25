using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Commands.UserCredit.UserCreditPay
{
    public class UserCreditPayCommandRequest : IRequest<UserCreditPayCommandResponse>
    {
        public int SenderID { get; set; }
        public int InstallementsID { get; set; }
        public decimal DelayInterest { get; set; }
    }
}
