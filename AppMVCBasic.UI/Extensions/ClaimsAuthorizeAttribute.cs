using Microsoft.AspNetCore.Mvc;
using static AppMVCBasic.UI.Extensions.CustomAuthorization;
using System.Security.Claims;

namespace AppMVCBasic.UI.Extensions
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequirementClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }
}
