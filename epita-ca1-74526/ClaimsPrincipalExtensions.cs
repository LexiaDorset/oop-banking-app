using System.Runtime.CompilerServices;
using System.Security.Claims;
// Name: Lucile Pelou
// Student number: 74526
namespace epita_ca1_74526
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
