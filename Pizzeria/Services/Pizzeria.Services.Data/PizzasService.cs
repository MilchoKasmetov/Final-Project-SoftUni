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
        private readonly IIngredientsService ingredientsService;

        public PizzasService(
            IDeletableEntityRepository<Pizza> pizzaRepository,
            IDeletableEntityRepository<Ingredient> ingredientRepository,
            IIngredientsService ingredientsService)
        {
            this.pizzaRepository = pizzaRepository;
            this.ingredientRepository = ingredientRepository;
            this.ingredientsService = ingredientsService;
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

        public async Task Delete(int id)
        {
            var pizza = await this.pizzaRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.pizzaRepository.Delete(pizza);
            await this.pizzaRepository.SaveChangesAsync();
        }

        public async Task<EditPizzaInputModel> GetForEditAsync(int id)
        {
            var pizza = await this.pizzaRepository.All().Include(x => x.Ingredients).FirstOrDefaultAsync(x => x.Id == id);
            var input = new EditPizzaInputModel()
            {
                Name = pizza.Name,
                ImageURL = pizza.ImageURL,
                DoughId = pizza.DoughId,
                SauceDipId = pizza.SauceDipId,
                SizeId = pizza.SizeId,
                Price = pizza.Price,
            };
            var allIngredientsList = await this.ingredientsService.GetIngredientsAsync();
            foreach (var ingredient in allIngredientsList)
            {
                foreach (var tickIngredient in pizza.Ingredients)
                {
                    if (tickIngredient.Id == ingredient.Id)
                    {
                        ingredient.Selected = true;
                    }
                }
            }

            input.Ingredients = allIngredientsList.ToArray();

            return input;
        }

        public async Task Restore(int id)
        {
            var pizza = await this.pizzaRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);
            this.pizzaRepository.Undelete(pizza);
            await this.pizzaRepository.SaveChangesAsync();
        }

        public async Task<ICollection<PizzaViewModel>> ShowAllDeletedPizzaAsync()
        {
            var allPizza = await this.pizzaRepository.AllWithDeleted().Include(x => x.Ingredients).Where(x => x.IsDeleted == true).ToListAsync();

            return allPizza.Select(x => new PizzaViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                ImageURL = x.ImageURL,
                Ingredients = string.Join(", ", x.Ingredients.Select(x => x.Name)),
            }).ToList();
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

        public async Task UpdateAsync(int id, EditPizzaInputModel input)
        {
            var pizza = await this.pizzaRepository.All().Include(x => x.Ingredients).FirstOrDefaultAsync(x => x.Id == id);
            pizza.Name = input.Name;
            pizza.ImageURL = input.ImageURL;
            pizza.DoughId = input.DoughId;
            pizza.SauceDipId = input.SauceDipId;
            pizza.SizeId = input.SizeId;
            pizza.Price = input.Price;
            pizza.Ingredients.Clear();

            foreach (var ingredient in input.Ingredients.Where(x => x.Selected == true))
            {
                var currentIngredient = await this.ingredientRepository.All().FirstAsync(x => x.Name == ingredient.Name);
                if (currentIngredient != null)
                {
                    pizza.Ingredients.Add(currentIngredient);
                }
            }

            await this.pizzaRepository.SaveChangesAsync();
        }
    }
}
