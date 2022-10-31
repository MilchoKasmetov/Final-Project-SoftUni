using Microsoft.EntityFrameworkCore;
using Pizzeria.Data.Common.Repositories;
using Pizzeria.Data.Models;
using Pizzeria.Web.ViewModels.Pizzas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzeria.Services.Data
{
    public class PizzasService : IPizzasService
    {
        private readonly IDeletableEntityRepository<Pizza> pizzaRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientRepository;

        public PizzasService(IDeletableEntityRepository<Pizza> pizzaRepository, IDeletableEntityRepository<Ingredient> ingredientRepository)
        {
            this.pizzaRepository = pizzaRepository;
            this.ingredientRepository = ingredientRepository;
        }

        public async Task CreatePizzaAsync(CreatePizzaInputModel input)
        {
            var pizza = new Pizza()
            {
                Name = input.Name,
                ImageURL = input.ImageURL,
                DoughId = input.DoughId,
                SauceDipId = input.SauceDipId,
                SizeId = input.SizeId,
                Price = input.Price,
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
    }
}
