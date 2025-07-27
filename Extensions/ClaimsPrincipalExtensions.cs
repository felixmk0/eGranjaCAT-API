using System.Security.Claims;

namespace nastrafarmapi.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) throw new Exception("User ID claim not found.");

            return userIdClaim.Value;
        }
    }
}
