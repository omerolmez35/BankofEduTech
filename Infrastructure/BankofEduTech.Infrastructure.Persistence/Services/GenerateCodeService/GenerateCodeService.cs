using BankofEduTech.Core.Application.Abstractions;
using BankofEduTech.Core.Application.Repositories;
using BankofEduTech.Core.Application.Repositories.CustomerAccount;
using BankofEduTech.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankofEduTech.Infrastructure.Persistence.Services.GenerateCodeService
{
    public class GenerateCodeService : IGenerateCodeService
    {
        private readonly ICustomerAccountReadRepository _customerAccountReadRepository;

        public GenerateCodeService(ICustomerAccountReadRepository customerAccountReadRepository)
        {
            _customerAccountReadRepository = customerAccountReadRepository;
        }

        public string AccountNumber(AccountType type, Currency currency, string TCNo)
        {
            var query = _customerAccountReadRepository.Table.Include(x => x.AppUser).Where(x => x.Currency == currency && x.AccountType == type && x.AppUser.IdentityNumber == TCNo).Max(x => x.AccountNumber);
            if (query != null)
            {
                int lastNumber = Convert.ToInt32(query.Substring(19, 3));
                lastNumber++;

                return $"{DateTime.Now.Year}{TCNo}0{(int)type}0{(int)currency}{lastNumber.ToString("D3")}";
            }
            else
                return $"{DateTime.Now.Year}{TCNo}0{(int)type}0{(int)currency}001";
        }
    }
}
