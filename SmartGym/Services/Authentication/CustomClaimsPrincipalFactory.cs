using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SmartGym.Models;
using System.Security.Claims;

public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, IdentityRole<int>>
{
    public CustomClaimsPrincipalFactory(
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole<int>> roleManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
    {
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);
        identity.AddClaim(new Claim("FirstName", user.FirstName ?? ""));
        identity.AddClaim(new Claim("LastName", user.LastName ?? ""));
        identity.AddClaim(new Claim("ImageRef", user.ImageRef ?? ""));

        return identity;
    }
}
