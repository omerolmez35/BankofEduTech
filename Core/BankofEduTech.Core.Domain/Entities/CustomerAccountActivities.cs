using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Domain.Entities
{
	public class CustomerAccountActivities : BaseEntity
	{
        public int ID { get; set; }
        public int CustomerAccountID { get; set; }
        [ForeignKey("CustomerAccountID")]
        public CustomerAccount CustomerAccount { get; set; }

       
        public ActivityType ActivityType { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public Guid ReferenceID { get; set; }


    }

    public enum ActivityType
    {
        Borc = 0,
        Alacak = 1
    }
}
