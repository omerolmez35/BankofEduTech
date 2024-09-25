using BankofEduTech.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Commands.UserMoney.SendMoney
{
    public class SendMoneyCommandRequest : IRequest<SendMoneyCommandResponse>
    {

        public int SenderID { get; set; }
        public int ReceiverID { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
    }
}
