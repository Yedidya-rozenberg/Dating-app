using System.Reflection.Metadata;
using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtantion
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {
         return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}