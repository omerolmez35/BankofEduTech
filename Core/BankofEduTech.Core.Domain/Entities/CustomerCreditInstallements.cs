using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Domain.Entities
{
    public class CustomerCreditInstallements : BaseEntity
    {
        [Key]
        public int InstallementsID { get; set; }
        public int NumberOfInstallements { get; set; }
        public string AmountOfInstallements { get; set; }
        public string AmountOfInterest { get; set; }
        public string AmountOfPrincipal { get; set; }
        public string RestOfPrincipal { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsPaid { get; set; }
        public int CustomerCreditID { get; set; }
        [ForeignKey("CustomerCreditID")]
        public CustomerCredits CustomerCredits { get; set; }
    }
}
