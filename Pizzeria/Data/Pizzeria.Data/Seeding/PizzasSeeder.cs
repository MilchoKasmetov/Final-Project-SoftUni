using Microsoft.EntityFrameworkCore;
using Pizzeria.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzeria.Data.Seeding
{
    public class PizzasSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Pizzas.Any())
            {
                return;
            }

            var allDoughs = await dbContext.Doughs.ToListAsync();
            var allSauceDips = await dbContext.SauceDips.ToListAsync();
            var allIngredients = await dbContext.Ingredients.ToListAsync();
            var allSizes = await dbContext.Sizes.ToListAsync();

            var dough = allDoughs.FirstOrDefault(x => x.Name == "Hand Tossed Traditional dough");
            var sauceDip = allSauceDips.FirstOrDefault(x => x.Name == "Tomato Sauce");
            var size = allSizes.FirstOrDefault(x => x.Name == "Large ∅ 30cm");
            //da se dobavi userId kogato seedna users i da go sloja predi tozi seeder
            await dbContext.Pizzas.AddAsync(new Pizza()
            {
                Name = "Margarita",
                ImageURL = "https://www.dominos.bg/gallery/fmobile/1265medium.png",
                DoughId = dough.Id,
                SauceDipId = sauceDip.Id,
                Ingredients = allIngredients,
                SizeId = size.Id,
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
