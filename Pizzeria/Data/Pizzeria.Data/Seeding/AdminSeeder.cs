namespace Pizzeria.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Pizzeria.Common;
    using Pizzeria.Data.Models;

    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var user = new ApplicationUser
            {
                UserName = GlobalConstants.AdministratorEmail,
                Email = GlobalConstants.AdministratorEmail,
                Address = GlobalConstants.AdminAddress,
            };

            var claim = new IdentityUserClaim<string>()
            {
                ClaimType = "Address",
                ClaimValue = GlobalConstants.AdminAddress,
            };

            user.Claims.Add(claim);

            var result = await userManager.CreateAsync(user, GlobalConstants.AdministratorPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
            }
        }
    }
}
