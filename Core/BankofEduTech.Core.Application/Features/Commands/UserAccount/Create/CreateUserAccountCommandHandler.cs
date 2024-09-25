using AutoMapper;
using BankofEduTech.Core.Application.Abstractions;
using BankofEduTech.Core.Application.Repositories;
using BankofEduTech.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;




namespace BankofEduTech.Core.Application.Features.Commands.UserAccount.Create
{
    public class CreateUserAccountCommandHandler : IRequestHandler<CreateUserAccountCommandRequest, CreateUserAccountCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUserAccountCommandHandler> _logger;




        public CreateUserAccountCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateUserAccountCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CreateUserAccountCommandResponse> Handle(CreateUserAccountCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var value = await _unitOfWork.AppUserReadRepository.Table.Include(x => x.CustomerAccounts).Where(x => x.Id == request.UserID || x.Id == request.AccountID).FirstOrDefaultAsync();


                CustomerAccount account = new CustomerAccount
                {
                    AccountName = request.AccountName,
                    AccountNumber = _unitOfWork.GenerateCodeService.AccountNumber(request.AccountType == 1 ? AccountType.Vadesiz : request.AccountType == 3 ? AccountType.Kredi : AccountType.Vadeli, request.Currency == 1 ? Currency.TRY : request.Currency == 2 ? Currency.USD : Currency.EUR, value.IdentityNumber),
                    Currency = request.Currency == 1 ? Currency.TRY : request.Currency == 2 ? Currency.USD : Currency.EUR,
                    AccountType = request.AccountType == 1 ? AccountType.Vadesiz : request.AccountType == 3 ? AccountType.Kredi : AccountType.Vadeli,
                    Balance = 0,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now

                };
                value.CustomerAccounts.Add(account);
                _unitOfWork.AppUserWriteRepository.Update(value);

                return new CreateUserAccountCommandResponse
                {
                    IsSuccess = true,
                    Message = "Hesap başarıyla oluşturuldu.",
                    AccountID = value.CustomerAccounts.OrderByDescending(x => x.ID).FirstOrDefault().ID,
                    AccountName = request.AccountName,
                    AccountType = request.AccountType == 1 ? "Vadesiz" : request.AccountType == 3 ? "Kredi" : "Vadeli",
                    Balance = account.Balance,
                    CreatedDate = account.CreatedDate.Date,
                    Currency = request.Currency == 1 ? "TRY" : request.Currency == 2 ? "USD" : "EUR",
                    AccountNumber = account.AccountNumber
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hata oluştu.");
                return new CreateUserAccountCommandResponse { IsSuccess = false, Message = "Hata oluştu!" };

            }
        }
    }
}
