namespace Pizzeria.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Pizzeria.Common;
    using Pizzeria.Data.Models;

    public class UserSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var user = new ApplicationUser
            {
                UserName = GlobalConstants.UserEmail,
                Email = GlobalConstants.UserEmail,
                Address = GlobalConstants.UserAddress,
            };

            var result = await userManager.CreateAsync(user, GlobalConstants.UserPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, GlobalConstants.UserRoleName);
            }
        }
    }
}
