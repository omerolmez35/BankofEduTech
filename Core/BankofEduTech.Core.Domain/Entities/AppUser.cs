using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Domain.Entities
{
	public class AppUser : IdentityUser<Guid>
	{
        public string Name { get; set; }
        public string Surname { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string ImageUrl { get; set; }
        public string IdentityNumber { get; set; }
        public List<CustomerAccount> CustomerAccounts { get; set; }

    }
}
