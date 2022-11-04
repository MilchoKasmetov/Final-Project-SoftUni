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
    using Pizzeria.Web.ViewModels.Dough;
    using Pizzeria.Web.ViewModels.IngredientCategories;
    using Pizzeria.Web.ViewModels.Ingredients;
    using Pizzeria.Web.ViewModels.Pizzas;

    public class IngredientsService : IIngredientsService
    {
        private readonly IDeletableEntityRepository<Ingredient> ingredientRepository;

        public IngredientsService(IDeletableEntityRepository<Ingredient> ingredientRepository)
        {
            this.ingredientRepository = ingredientRepository;
        }

        public async Task CreateIngredientsAsync(CreateIngredientInputModel model)
        {
            var ingredient = new Ingredient()
            {
                Name = model.Name,
                IngredientCategoryId = model.IngredientCategoryId,
            };

            var allIngredients = await this.ingredientRepository.All().ToListAsync();

            if (!allIngredients.Any(x => x.Name == model.Name))
            {
                await this.ingredientRepository.AddAsync(ingredient);
                await this.ingredientRepository.SaveChangesAsync();
            }
        }

        public async Task<ICollection<IngredientViewModel>> GetAllIngredientsAsync()
        {
            return await this.ingredientRepository.All().Select(x => new IngredientViewModel() { Id = x.Id, Name = x.Name, IngredientCategory = new IngredientCategoryViewModel() { Name = x.IngredientCategory.Name } }).OrderBy(x => x.IngredientCategory.Name).ThenBy(x => x.Name).ToListAsync();

        }

        public async Task<ICollection<PizzaIngredientInputModel>> GetIngredientsAsync()
        {
            return await this.ingredientRepository.AllAsNoTracking().Select(x => new PizzaIngredientInputModel() { Id = x.Id, Name = x.Name, IngredientCategoryName = x.IngredientCategory.Name }).OrderBy(x => x.IngredientCategoryName).ThenBy(x => x.Name).ToListAsync();
        }
    }
}
