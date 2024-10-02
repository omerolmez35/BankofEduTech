using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetPaidPaymentsForCustomer
{
    public class GetPaidPaymentsForCustomerQueryRequest : IRequest<List<GetPaidPaymentsForCustomerQueryResponse>>
    {
        public Guid UserID { get; set; }

        public GetPaidPaymentsForCustomerQueryRequest(Guid userID)
        {
            UserID = userID;
        }
    }
}
