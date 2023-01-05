using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppMVCBasic.UI.Extensions
{
    public class CustomAuthorization
    {
        public static bool ValidateUserClaims(HttpContext context, string claimName, string claimValue)
        {
            return context.User.Identity.IsAuthenticated &&
                   context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
        }
        
        
    }
}
