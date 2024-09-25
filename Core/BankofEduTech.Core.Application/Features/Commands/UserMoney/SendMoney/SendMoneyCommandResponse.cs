using BankofEduTech.Core.Application.ViewModels;

namespace BankofEduTech.Core.Application.Features.Commands.UserMoney.SendMoney
{
    public class SendMoneyCommandResponse : ResultModel
    {
        public Guid SenderID { get; set; }
        public Guid ReceiverID { get; set; }
    }
}
