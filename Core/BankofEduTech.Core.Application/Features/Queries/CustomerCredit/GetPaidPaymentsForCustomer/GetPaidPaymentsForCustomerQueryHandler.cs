using BankofEduTech.Core.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetPaidPaymentsForCustomer
{
    public class GetPaidPaymentsForCustomerQueryHandler : IRequestHandler<GetPaidPaymentsForCustomerQueryRequest, List<GetPaidPaymentsForCustomerQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPaidPaymentsForCustomerQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<List<GetPaidPaymentsForCustomerQueryResponse>> Handle(GetPaidPaymentsForCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            var payments = _unitOfWork.CustomerCreditReadRepository.Table.Include(x => x.CustomerCreditInstallements).Where(x => x.UserID == request.UserID).ToList();
            if (payments != null)
            {
                List<GetPaidPaymentsForCustomerQueryResponse> response = new List<GetPaidPaymentsForCustomerQueryResponse>();
                foreach (var payment in payments)
                {
                    foreach (var item in payment.CustomerCreditInstallements.Where(x => x.ExpireDate < DateTime.Now.Date || x.IsPaid))
                    {
                        response.Add(new GetPaidPaymentsForCustomerQueryResponse
                        {
                            AmountOfInstallements = item.AmountOfInstallements,
                            IsLate =  item.ExpireDate.Date < item.PaymentDate.Date && item.IsPaid ? true : !item.IsPaid && item.ExpireDate.Date < DateTime.Now.Date ? true : false,
                            NumberOfInstallements = item.NumberOfInstallements
                        });
                    }
                }
                return Task.FromResult(response);
                
            }
            return Task.FromResult(new List<GetPaidPaymentsForCustomerQueryResponse>());
        }
    }
}
