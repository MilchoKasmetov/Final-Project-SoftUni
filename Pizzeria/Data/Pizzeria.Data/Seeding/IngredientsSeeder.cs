namespace Pizzeria.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Pizzeria.Data.Models;

    public class IngredientsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Ingredients.Any())
            {
                return;
            }

            var allCategory = await dbContext.IngredientCategories.ToListAsync();

            var herbsCategory = allCategory.FirstOrDefault(x => x.Name == "Herbs");
            var cheesesCategory = allCategory.FirstOrDefault(x => x.Name == "Cheeses");
            var meatsCategory = allCategory.FirstOrDefault(x => x.Name == "Meats");
            var vegetablesCategory = allCategory.FirstOrDefault(x => x.Name == "Vegetables");

            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Oregano", IngredientCategoryId = herbsCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Parmesan Sprinkles", IngredientCategoryId = herbsCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Basil", IngredientCategoryId = herbsCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Pesto Sauce", IngredientCategoryId = herbsCategory.Id });

            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Feta Cheese", IngredientCategoryId = cheesesCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Cheddar Cheese", IngredientCategoryId = cheesesCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Vegan Mozzarella", IngredientCategoryId = cheesesCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Parmesan", IngredientCategoryId = cheesesCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Mozzarella", IngredientCategoryId = cheesesCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Emmental", IngredientCategoryId = cheesesCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Smoked Melted Cheese", IngredientCategoryId = cheesesCategory.Id });

            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Ventrichina", IngredientCategoryId = meatsCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Chicken", IngredientCategoryId = meatsCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Choriso", IngredientCategoryId = meatsCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Tuna", IngredientCategoryId = meatsCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Smoked Bacon", IngredientCategoryId = meatsCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Smoked Ham", IngredientCategoryId = meatsCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Spicy Beef", IngredientCategoryId = meatsCategory.Id });

            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Jalapenos Peppers", IngredientCategoryId = vegetablesCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Fresh green peppers", IngredientCategoryId = vegetablesCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Pineapple", IngredientCategoryId = vegetablesCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Ruccula", IngredientCategoryId = vegetablesCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Fresh mushrooms", IngredientCategoryId = vegetablesCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Pickless", IngredientCategoryId = vegetablesCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Onion", IngredientCategoryId = vegetablesCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Black Olives", IngredientCategoryId = vegetablesCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Corn", IngredientCategoryId = vegetablesCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Fresh tomato", IngredientCategoryId = vegetablesCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Caramelized onions", IngredientCategoryId = vegetablesCategory.Id });
            await dbContext.Ingredients.AddAsync(new Ingredient() { Name = "Sun-dried tomatoes in oil", IngredientCategoryId = vegetablesCategory.Id });

            await dbContext.SaveChangesAsync();
        }
    }
}
