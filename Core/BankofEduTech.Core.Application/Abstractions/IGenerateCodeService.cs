using BankofEduTech.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Abstractions
{
    public interface IGenerateCodeService
    {
        string AccountNumber(AccountType type, Currency currency, string TCNo);
    }
}
