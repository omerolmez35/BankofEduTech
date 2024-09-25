using BankofEduTech.Core.Application.ViewModels;

namespace BankofEduTech.Core.Application.Features.Queries.AppUser.LoginUser
{
    public class LoginUserQueryResponse : ResultModel
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        
        public string FullName {
            get
            {
                return $"{Name} {Surname}";
            }
        }
        public string Username { get; set; }
        public string UserID { get; set; }
        public IList<string> Roles { get; set; }
    }

}
