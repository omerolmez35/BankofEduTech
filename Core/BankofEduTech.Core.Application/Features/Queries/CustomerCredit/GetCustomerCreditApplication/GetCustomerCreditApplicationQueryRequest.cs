using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetCustomerCreditApplication
{
    public class GetCustomerCreditApplicationQueryRequest : IRequest<List<GetCustomerCreditApplicationQueryResponse>>
    {
    }
}
