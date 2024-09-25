using BankofEduTech.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Commands.Notification.Create
{
    public class CreateNotificiationCommandRequest : IRequest<CreateNotificiationCommandResponse>
    {
        public Guid UserID { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public NotificationType NotificationType { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
