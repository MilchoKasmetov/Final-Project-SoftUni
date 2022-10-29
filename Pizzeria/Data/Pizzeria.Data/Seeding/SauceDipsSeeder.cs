namespace Pizzeria.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Pizzeria.Data.Models;

    public class SauceDipsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.SauceDips.Any())
            {
                return;
            }

            await dbContext.SauceDips.AddAsync(new SauceDip() { Name = "Tomato Sauce" });
            await dbContext.SauceDips.AddAsync(new SauceDip() { Name = "BBQ Sauce" });
            await dbContext.SauceDips.AddAsync(new SauceDip() { Name = "Cream Sauce" });     

            await dbContext.SaveChangesAsync();
        }
    }
}
