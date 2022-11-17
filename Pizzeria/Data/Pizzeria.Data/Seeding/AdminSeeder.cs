namespace Pizzeria.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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

            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser
                {
                    Email = GlobalConstants.AdministratorEmail,
                    ShoppingCart = new ShoppingCart(),
                };

                var result = await userManager.CreateAsync(user, GlobalConstants.AdministratorPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
                }

                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
            }

        }
    }
}
