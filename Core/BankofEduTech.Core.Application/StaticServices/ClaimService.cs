namespace BankofEduTech.Core.Application.StaticServices
{
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.Data;
    using System.Security.Claims;

    namespace BankofEduTech.Core.Application.StaticServices
    {
        public class ClaimService
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            public ClaimService(IHttpContextAccessor httpContextAccessor)
            {
                _httpContextAccessor = httpContextAccessor;
            }

            private ClaimsPrincipal UserClaims => _httpContextAccessor.HttpContext?.User;

            public long UserId
            {
                get
                {
                    return TryGetClaimValue("UserId", out string value) ? Convert.ToInt64(value) : 0;
                }
            }

            public string UserName
            {
                get
                {
                    return TryGetClaimValue(ClaimTypes.NameIdentifier, out string value) ? value : ClaimTypes.NameIdentifier;
                }
            }

            public string Email
            {
                get
                {
                    return TryGetClaimValue("Email", out string value) ? value : "System";
                }
            }

            public string Name
            {
                get
                {
                    return TryGetClaimValue(ClaimTypes.Name, out string value) ? value : "System";
                }
            }

            public string Surname
            {
                get
                {
                    return TryGetClaimValue(ClaimTypes.Surname, out string value) ? value : ClaimTypes.Surname;
                }
            }

            public string FullName
            {
                get
                {
                    return TryGetClaimValue("FullName", out string value) ? value : $"{Name} {Surname}";
                }
            }

            public bool IsSuperAdmin
            {
                get
                {
                    return TryGetClaimValue("IsSuperAdmin", out string value) && bool.TryParse(value, out bool isSuperAdmin) ? isSuperAdmin : false;
                }
            }

            public string ImagePath
            {
                get
                {
                    return TryGetClaimValue("ImagePath", out string value) ? value : "System";
                }
            }

            public string IPAddress
            {
                get
                {
                    return TryGetClaimValue("IPAddress", out string value) ? value : "Unknown";
                }
            }

            public string MACAddress
            {
                get
                {
                    return TryGetClaimValue("MACAddress", out string value) ? value : "Unknown";
                }
            }

            public string Role
            {
                get
                {
                    return TryGetClaimValue(ClaimTypes.Role, out string value) ? value : ClaimTypes.Role;
                }
            }

            private bool TryGetClaimValue(string claimType, out string claimValue)
            {
                claimValue = UserClaims?.FindFirst(claimType)?.Value;
                return claimValue != null;
            }

            public Dictionary<string, object> GetAllClaims()
            {
                return new Dictionary<string, object>
            {
                { "UserId", UserId },
                { "UserName", UserName },
                { "Email", Email },
                { "Name", Name },
                { "Surname", Surname },
                { "FullName", FullName },
                { "IsSuperAdmin", IsSuperAdmin },
                { "ImagePath", ImagePath },
                { "IPAddress", IPAddress },
                { "MACAddress", MACAddress },
                { "Role", Role }
            };
            }

            public static ClaimService CurrentUser
            {
                get
                {
                    var httpContextAccessor = new HttpContextAccessor();
                    return new ClaimService(httpContextAccessor);
                }
            }
        }
    }



}
