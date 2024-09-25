using AutoMapper.Configuration.Annotations;
using BankofEduTech.Core.Application.Abstractions;
using BankofEduTech.Core.Application.Repositories;
using BankofEduTech.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Infrastructure.Infrastructure.Services.SeedService
{
    public class SeedService : ISeedService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public SeedService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        public async Task SeedRolesAndAdminAsync()
        {
            try
            {
                var roles = new List<string> { "Admin", "Customer" };

                foreach (var role in roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new AppRole { Name = role });
                    }
                }

             
                var adminEmail = "admin@example.com";
                var adminPassword = "Admin123!";

                if (await _userManager.FindByEmailAsync(adminEmail) == null)
                {
                    var adminUser = new AppUser
                    {
                        UserName = "admin",
                        Email = adminEmail,
                        Name = "Admin",
                        IdentityNumber = "00000000000",
                        Surname = "Admin",
                        City = "",
                        District = "",
                        ImageUrl = "",

                        EmailConfirmed = true
                    };

                    var result = await _userManager.CreateAsync(adminUser, adminPassword);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(adminUser, "Admin");
                    }



                    var accountControl = await _unitOfWork.CustomerAccountReadRepository.GetSingleAsync(x => x.AccountName == "BANKA KASASI");
                    if (accountControl == null)
                    {
                        CustomerAccountActivities act = new CustomerAccountActivities
                        {
                            Amount = 100000000,
                            CreatedDate = DateTime.Now,
                            ActivityType = ActivityType.Alacak,
                            IsActive = true,
                            IsDeleted = false,
                            Description = "Sermaye Yüklemesi",
                            ReferenceID = Guid.NewGuid()
                        };
                        CustomerAccount firstAccount = new CustomerAccount
                        {
                            AccountName = "BANKA KASASI",
                            AccountNumber = $"{DateTime.Now.Year}000000000000101001",
                            AccountType = AccountType.Vadesiz,
                            AppUser = adminUser,
                            Balance = 100000000,
                            IsActive = true,
                            IsDeleted = false,
                            CreatedDate = DateTime.Now,
                            Currency = Currency.TRY,
                            CustomerAccountActivities = new List<CustomerAccountActivities> { act },

                        };

                        await _unitOfWork.CustomerAccountWriteRepository.AddAsync(firstAccount);
                    }




                }


                
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
