using System.Security.Claims;

namespace nastrafarmapi.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst("id");
            if (userIdClaim == null) throw new Exception("User ID claim not found.");

            return int.Parse(userIdClaim.Value);
        }
    }
}
