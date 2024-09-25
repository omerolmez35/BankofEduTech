using AutoMapper;
using BankofEduTech.Core.Application.Features.Commands.AppUser.Create;
using BankofEduTech.Core.Application.Features.Commands.Notification.Create;
using BankofEduTech.Core.Application.Features.Commands.UserCredit.CreateUserCredit;
using BankofEduTech.Core.Application.Features.Commands.UserCredit.CreateUserCreditApplication;
using BankofEduTech.Core.Application.Features.Queries.AppUser;
using BankofEduTech.Core.Application.Features.Queries.AppUser.GetUserByName;
using BankofEduTech.Core.Application.Features.Queries.Customer.GetAllCustomer;
using BankofEduTech.Core.Application.Features.Queries.CustomerCredit.CustomerCreditCalculation;
using BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetApplicationByUserId;
using BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetCreditPayments;
using BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetCustomerCreditApplication;
using BankofEduTech.Core.Application.Features.Queries.Notification.GetActiveLastNotification;
using BankofEduTech.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Mapping
{
    public class GeneralMapping : Profile
	{
        public GeneralMapping()
        {
            CreateMap<AppUser, CreateAppUserCommandRequest>().ReverseMap();
            CreateMap<AppUser, GetUserByNameQueryResponse>().ReverseMap();
            CreateMap<CreateUserCreditCommandRequest, CustomerCreditCalculationQueryRequest>().ReverseMap();
            CreateMap<CustomerCreditInstallements, CustomerCreditCalculationQueryResponse>().ReverseMap();
            CreateMap<CustomerCreditInstallements, GetCreditPaymentsQueryResponse>().ReverseMap();
            CreateMap<CustomerCreditInstallements, CreateUserCreditApplicationCommandRequest>().ReverseMap();
            CreateMap<AppUser, GetAllCustomerQueryResponse>().ReverseMap();
            CreateMap<Notifications, GetActiveLastNotificationQueryResponse>().ReverseMap();
            CreateMap<Notifications, CreateNotificiationCommandRequest>().ReverseMap();
            CreateMap<CustomerCredits, GetApplicationByUserIdQueryResponse>().ReverseMap();
            // CreateMap<CustomerCredits, GetCustomerCreditApplicationQueryResponse>().ReverseMap();

            CreateMap<CustomerCredits, GetCustomerCreditApplicationQueryResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.AppUser.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.AppUser.Surname));
        }
    }
}
