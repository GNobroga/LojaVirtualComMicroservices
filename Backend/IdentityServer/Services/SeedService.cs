using System.Security.Claims;
using IdentityModel;
using IdentityServer.Configurations;
using IdentityServer.Data;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Services;

public class SeedService
{   
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly RoleManager<IdentityRole> _roleManager;

    public SeedService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitializeRoles()
    {
        if (await _roleManager.RoleExistsAsync(IdentityConfiguration.ADMIN))
        {
            IdentityRole role = new() { Name = IdentityConfiguration.ADMIN };
            await _roleManager.CreateAsync(role);
        }

        if (await _roleManager.RoleExistsAsync(IdentityConfiguration.CLIENT))
        {
            IdentityRole role = new() { Name = IdentityConfiguration.CLIENT };
            await _roleManager.CreateAsync(role);
        }
    }

    public async Task InitializeUser()
    {
        if ((await _userManager.FindByEmailAsync("admin@admin.com")) is null)
        {
            ApplicationUser admin = new()
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "+55 (28) 99950-5410",
                FirstName = "Gabriel",
                LastName = "Cardoso",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult result = await _userManager.CreateAsync(admin, "Gabrielcar34@");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(admin, IdentityConfiguration.ADMIN);

                await _userManager.AddClaimsAsync(admin, new Claim[] {
                    new(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                    new(JwtClaimTypes.GivenName, admin.FirstName),
                    new(JwtClaimTypes.FamilyName, admin.LastName),
                    new(JwtClaimTypes.Role, IdentityConfiguration.ADMIN),
                });
            }
        }
    }
}