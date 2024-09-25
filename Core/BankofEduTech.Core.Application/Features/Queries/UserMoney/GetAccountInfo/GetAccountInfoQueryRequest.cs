using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.UserMoney.GetAccountInfo
{
    public class GetAccountInfoQueryRequest : IRequest<GetAccountInfoQueryResponse>
    {
        public string AccountNumber { get; set; }
        public int SenderAccountNumberID { get; set; }

    }
}
