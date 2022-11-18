namespace Pizzeria.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Pizzeria.Data.Models;

    public class DoughsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Doughs.Any())
            {
                return;
            }

            await dbContext.Doughs.AddAsync(new Dough() { Name = "Hand Tossed Traditional dough" });
            await dbContext.Doughs.AddAsync(new Dough() { Name = "Thin Italian Style dough" });
            await dbContext.Doughs.AddAsync(new Dough() { Name = "Hand-tossed dough with mozzarella stuffed crust" });
            await dbContext.Doughs.AddAsync(new Dough() { Name = "Fresh dough stuffed with Philadelphia cream cheese" });
            await dbContext.Doughs.AddAsync(new Dough() { Name = "Thin & Crispy dough" });
            await dbContext.Doughs.AddAsync(new Dough() { Name = "Hand-tossed dough with pepperoni stuffed crust" });

            await dbContext.SaveChangesAsync();
        }
    }
}
