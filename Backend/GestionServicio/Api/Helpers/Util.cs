using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Helpers
{
    public static class Util
    {
        public static int GetUserIdClainToken(HttpContext httpContext)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
                throw new Exception("La identidad del usuario es null");

            var userClaims = identity.Claims;
            var userIdClaim = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            if (userIdClaim == null)
                throw new Exception("El claim del usuario es null");

            return int.Parse(userIdClaim.Value);
        }
    }
}
