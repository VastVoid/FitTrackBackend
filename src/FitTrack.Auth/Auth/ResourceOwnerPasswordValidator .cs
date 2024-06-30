using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using FitTrack.Auth.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FitTrack.Auth.Auth;

public class ResourceOwnerPasswordValidator(ApplicationDbContext context) : IResourceOwnerPasswordValidator
{
    private readonly ApplicationDbContext _context = context;

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var user = await _context.Users
            .Include(item => item.UserRoles)
            .ThenInclude(item => item.Role)
            .FirstOrDefaultAsync(item => item.Username == context.UserName && item.Password == context.Password);

        if (user != null)
        {
            var claims = user.UserRoles.Select(userRole => new Claim("role", userRole.Role!.Name)).ToList();

            context.Result = new GrantValidationResult(
                subject: user.Username,
                authenticationMethod: "custom",
                claims: claims);

            return;
        }

        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid credentials");
    }
}
