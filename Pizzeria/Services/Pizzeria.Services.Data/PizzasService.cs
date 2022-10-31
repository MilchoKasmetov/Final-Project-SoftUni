namespace Pizzeria.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Pizzeria.Data.Common.Repositories;
    using Pizzeria.Data.Models;
    using Pizzeria.Web.ViewModels.Pizzas;

    public class PizzasService : IPizzasService
    {
        private readonly IDeletableEntityRepository<Pizza> pizzaRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientRepository;

        public PizzasService(IDeletableEntityRepository<Pizza> pizzaRepository, IDeletableEntityRepository<Ingredient> ingredientRepository)
        {
            this.pizzaRepository = pizzaRepository;
            this.ingredientRepository = ingredientRepository;
        }

        public async Task CreatePizzaAsync(CreatePizzaInputModel input, string userId)
        {
            var pizza = new Pizza()
            {
                Name = input.Name,
                ImageURL = input.ImageURL,
                DoughId = input.DoughId,
                SauceDipId = input.SauceDipId,
                SizeId = input.SizeId,
                Price = input.Price,
                AddedByUserId = userId,
            };

            foreach (var ingredient in input.Ingredients.Where(x => x.Selected == true))
            {
                var currentIngredient = await this.ingredientRepository.All().FirstAsync(x => x.Name == ingredient.Name);
                if (currentIngredient != null)
                {
                    pizza.Ingredients.Add(currentIngredient);
                }
            }

            await this.pizzaRepository.AddAsync(pizza);
            await this.pizzaRepository.SaveChangesAsync();
        }

        public async Task<ICollection<PizzaViewModel>> ShowAllPizzaAsync()
        {

            var allPizza = await this.pizzaRepository.AllAsNoTracking().Include(x => x.Ingredients).ToListAsync();

            return allPizza.Select(x => new PizzaViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                ImageURL = x.ImageURL,
                Ingredients = string.Join(", ", x.Ingredients.Select(x => x.Name)),
            }).ToList();
        }
    }
}
