using BankofEduTech.Core.Application.Mapping;
using BankofEduTech.Core.Application.StaticServices.BankofEduTech.Core.Application.StaticServices;
using BankofEduTech.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application
{
    public static class Registration
    {
        public static void AddApplicationRegistration(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddScoped<ClaimService>();
            services.AddAutoMapper(assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        }


    }
}
