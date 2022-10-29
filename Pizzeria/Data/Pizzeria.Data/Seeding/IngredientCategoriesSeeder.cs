namespace Pizzeria.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Pizzeria.Data.Models;

    public class IngredientCategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.IngredientCategories.Any())
            {
                return;
            }

            await dbContext.IngredientCategories.AddAsync(new IngredientCategory() { Name = "Herbs" });
            await dbContext.IngredientCategories.AddAsync(new IngredientCategory() { Name = "Cheeses" });
            await dbContext.IngredientCategories.AddAsync(new IngredientCategory() { Name = "Meats" });
            await dbContext.IngredientCategories.AddAsync(new IngredientCategory() { Name = "Vegetables" });

            await dbContext.SaveChangesAsync();
        }
    }
}
