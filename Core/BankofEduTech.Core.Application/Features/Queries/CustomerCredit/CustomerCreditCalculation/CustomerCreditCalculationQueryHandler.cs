using BankofEduTech.Core.Application.Features.Queries.CustomerCredit.CustomerCreditCalculation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankofEduTech.Core.Application.Features.Queries.CustomerCredit.CustomerCreditCalculation
{
    public class CustomerCreditCalculationQueryHandler : IRequestHandler<CustomerCreditCalculationQueryRequest, List<CustomerCreditCalculationQueryResponse>>
    {
        private readonly ILogger<CustomerCreditCalculationQueryHandler> _logger;

        public CustomerCreditCalculationQueryHandler(ILogger<CustomerCreditCalculationQueryHandler> logger)
        {
            _logger = logger;
        }

        public async Task<List<CustomerCreditCalculationQueryResponse>> Handle(CustomerCreditCalculationQueryRequest request, CancellationToken cancellationToken)
        {

            try
            {
                double monthlyPayment = request.AmountOfLoan * ((request.InterestRate / 100) * Math.Pow(1 + (request.InterestRate / 100), request.NumberofInstallement)) / (Math.Pow(1 + (request.InterestRate / 100), request.NumberofInstallement) - 1);
                List<CustomerCreditCalculationQueryResponse> table = new List<CustomerCreditCalculationQueryResponse>();
                double restOfPrincipal = request.AmountOfLoan;
                for (int i = 1; i <= request.NumberofInstallement; i++)
                {
                    double interestPayment = restOfPrincipal * (request.InterestRate / 100);
                    double principalPayment = monthlyPayment - interestPayment;

                    restOfPrincipal -= principalPayment;
                    table.Add(new CustomerCreditCalculationQueryResponse
                    {
                        NumberOfInstallements = i,
                        AmountOfInstallements = monthlyPayment.ToString("0.##"),
                        AmountOfInterest = interestPayment.ToString("0.##"),
                        AmountOfPrincipal = principalPayment.ToString("0.##"),
                        RestOfPrincipal = restOfPrincipal < 0 ? "0" : restOfPrincipal.ToString("0.##"),
                        IsSuccess = true,
                        Message = "İşlem başarılı",
                        ExpireDate = DateTime.Now.AddDays(i * 30).ToShortDateString(),
                    });

                }
                return table;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hata oluştu.");
                throw;
            }
            
        }
    }
}
