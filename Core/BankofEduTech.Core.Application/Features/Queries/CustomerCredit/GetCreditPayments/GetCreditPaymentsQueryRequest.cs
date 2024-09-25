using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetCreditPayments
{
    public class GetCreditPaymentsQueryRequest : IRequest<List<GetCreditPaymentsQueryResponse>>
    {
        public int AccountID { get; set; }

        public GetCreditPaymentsQueryRequest(int accountID)
        {
            AccountID = accountID;
        }
    }
}
