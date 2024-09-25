using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Domain.Entities
{
    public class CustomerCredits : BaseEntity
    {
        [Key]
        public int CreditID { get; set; }
        public int NumberofInstallement { get; set; }
        public DateTime LastInstallementDate { get; set; }
        public decimal AmountOfInstallement { get; set; }
        public decimal AmountOfLoan { get; set; }
        public decimal TotalBackPaymnent { get; set; }
        public int? CustomerAccountID { get; set; }
        [ForeignKey("CustomerAccountID")]
        public CustomerAccount? CustomerAccount { get; set; }
        public List<CustomerCreditInstallements> CustomerCreditInstallements { get; set; }
        public CreditStatus CreditStatus { get; set; }
        public decimal InterestRate { get; set; }
        public int PaymentAccountID { get; set; }
        public Guid UserID { get; set; }
        [ForeignKey("UserID")]
        public AppUser AppUser { get; set; }
        public int CountofInstallements { get; set; }


    }

    public enum CreditStatus
    {
        Beklemede = 0,
        Onay = 1,
        Red = 2
    }
}
