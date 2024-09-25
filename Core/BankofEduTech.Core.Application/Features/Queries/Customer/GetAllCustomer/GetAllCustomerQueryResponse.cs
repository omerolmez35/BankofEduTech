using BankofEduTech.Core.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.Customer.GetAllCustomer
{
    public class GetAllCustomerQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
        public string IdentityNumber { get; set; }
    }
}
