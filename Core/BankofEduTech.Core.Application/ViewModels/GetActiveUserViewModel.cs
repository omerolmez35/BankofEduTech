using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.ViewModels
{
    public class GetActiveUserViewModel
    {

        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

    }
}
