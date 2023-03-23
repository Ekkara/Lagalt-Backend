using System.Security.Claims;

namespace Lagalt_Backend.Helpers
{
    public static class UserHelper
    {
        public static string? GetId(this ClaimsPrincipal principal)
        {
            var p = principal;
            if (p != null)
            {
                return p.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            return null;

        }

        public static string? GetUsername(this ClaimsPrincipal principal)
        {
            var p = principal;
            if (p != null)
            {
                return p.FindFirstValue("preferrred_username");
            }
            return null;
        }
    }
}
