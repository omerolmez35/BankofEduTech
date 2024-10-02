using BankofEduTech.Core.Application.ViewModels;


namespace BankofEduTech.Core.Application.Features.Queries.AppUser.GetUserByName
{
    public class GetUserByNameQueryResponse : ResultModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
    }
}
