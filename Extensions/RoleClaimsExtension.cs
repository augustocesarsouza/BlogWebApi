using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BlogWeb.Extensions
{
    public static class RoleClaimsExtension
    {
        public static IEnumerable<Claim> GetClaims(this User user) 
        {
            var result = new List<Claim> // Lista com os Objetos Key Value
            {
                new(ClaimTypes.Name, user.Email)
            };
            result.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Slug)));

            return result;
        }
    }
}
