using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ArQr.Models;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace ArQr.IdentityServer
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser>                 _userManager;

        public ProfileService(UserManager<ApplicationUser>                 userManager)
        {
            _userManager   = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub       = context.Subject.GetSubjectId();
            var user      = await _userManager.FindByIdAsync(sub);

            var claims = new List<Claim>
            {
                new Claim("UserName",             user.UserName),
                new Claim("Email",                user.Email),
                new Claim("EmailConfirmed",       user.EmailConfirmed.ToString()),
                new Claim("PhoneNumber",          user.PhoneNumber ?? string.Empty),
                new Claim("PhoneNumberConfirmed", user.PhoneNumberConfirmed.ToString())
            };

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub  = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}