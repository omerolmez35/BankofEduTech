using BankofEduTech.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Commands.UserAccount.Create
{
    public class CreateUserAccountCommandRequest : IRequest<CreateUserAccountCommandResponse>
    {
        public Guid? UserID { get; set; }
        public Guid? AccountID { get; set; }
        public int Currency { get; set; }
        public int AccountType { get; set; }
        public string AccountName { get; set; }
       
    }
}
