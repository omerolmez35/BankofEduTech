using BankofEduTech.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Infrastructure.Persistence.Context
{
	public class BankofEduTechContext : IdentityDbContext<AppUser, AppRole, Guid>
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=127.0.0.1,1433; uid = sa; pwd = Omer1234;Database=BankofEduTechDB;Trusted_Connection=False;Trust Server Certificate=True;");
		}

		public DbSet<CustomerAccount> CustomerAccounts { get; set; }
		public DbSet<CustomerAccountActivities> CustomerAccountActivities { get; set; }
		public DbSet<CustomerCredits> CustomerCredits { get; set; }
		public DbSet<CustomerCreditInstallements> CustomerCreditInstallements { get; set; }
		public DbSet<Notifications> Notifications { get; set; }

      
    }
}
