using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using FitTrack.Auth.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FitTrack.Auth.Auth;

public class ProfileService(ApplicationDbContext context) : IProfileService
{
    private readonly ApplicationDbContext _context = context;

    public Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        if (context.Subject.Identity != null)
        {
            var user = _context.Users
                .Include(item => item.UserRoles)
                .ThenInclude(item => item.Role)
                .FirstOrDefault(item => item.Username == context.Subject.Identity.Name);

            if (user != null)
            {
                var claims = user.UserRoles.Select(userRole => new Claim("role", userRole.Role!.Name)).ToList();
                context.IssuedClaims.AddRange(claims);
            }
        }

        return Task.FromResult(0);
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        context.IsActive = true;
        return Task.FromResult(0);
    }
}
