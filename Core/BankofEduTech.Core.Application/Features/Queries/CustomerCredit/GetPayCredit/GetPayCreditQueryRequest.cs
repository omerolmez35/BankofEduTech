using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetPayCredit
{
    public class GetPayCreditQueryRequest : IRequest<GetPayCreditQueryResponse>
    {
        public int InstallementsID { get; set; }
        public Guid UserID { get; set; }

        public GetPayCreditQueryRequest(int ınstallementsID, Guid userID)
        {
            InstallementsID = ınstallementsID;
            UserID = userID;
        }
    }
}
