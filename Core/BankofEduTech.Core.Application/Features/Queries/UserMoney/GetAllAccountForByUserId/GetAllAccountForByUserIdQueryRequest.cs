using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.UserMoney.GetAllAccountForByUserId
{
    public class GetAllAccountForByUserIdQueryRequest : IRequest<List<GetAllAccountForByUserIdQueryResponse>>
    {
        public string? UserName { get; set; }
        public Guid? UserID { get; set; }

    }
}
