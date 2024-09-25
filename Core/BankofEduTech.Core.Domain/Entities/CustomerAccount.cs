using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Domain.Entities
{
    public class CustomerAccount : BaseEntity
    {
        public int ID { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public Currency Currency { get; set; }
        public AccountType AccountType { get; set; }
        public decimal Balance { get; set; }
        public Guid AppUserID { get; set; }
        [ForeignKey("AppUserID")]
        public AppUser AppUser { get; set; }
        public List<CustomerAccountActivities> CustomerAccountActivities { get; set; }
        public List<CustomerCredits> CustomerCredit { get; set; }

    }

    public enum Currency
    {
        TRY = 1,
        USD = 2,
        EUR = 3
    }

    public enum AccountType
    {
        Vadesiz = 1,
        Vadeli = 2,
        Kredi = 3
    }

}
