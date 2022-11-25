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

            if (!allIngredients.Any(x => x.Name == model.Name) && model.Name != null)
            {
                await this.ingredientRepository.AddAsync(ingredient);
                await this.ingredientRepository.SaveChangesAsync();
            }
        }

        public async Task<ICollection<IngredientViewModel>> GetAllIngredientsAsync()
        {
            return await this.ingredientRepository.All().Select(x => new IngredientViewModel() { Id = x.Id, Name = x.Name, IngredientCategory = new IngredientCategoryViewModel() { Name = x.IngredientCategory.Name } }).OrderBy(x => x.IngredientCategory.Name).ThenBy(x => x.Name).ToListAsync();
        }

        public async Task<EditIngredientInputModel> GetForUpdateAsync(int id)
        {
            var ingredient = await this.ingredientRepository.AllWithDeleted().Include(x => x.IngredientCategory).FirstOrDefaultAsync(x => x.Id == id);
            var input = new EditIngredientInputModel()
            {
                Name = ingredient.Name,
                IngredientCategoryId = ingredient.IngredientCategory.Id,
                IngredientCategory = new IngredientCategoryViewModel() { Id = ingredient.IngredientCategoryId, Name = ingredient.IngredientCategory.Name},
            };

            return input;
        }

        public async Task<ICollection<PizzaIngredientInputModel>> GetIngredientsAsync()
        {
            return await this.ingredientRepository.AllAsNoTracking().Select(x => new PizzaIngredientInputModel() { Id = x.Id, Name = x.Name, IngredientCategoryName = x.IngredientCategory.Name }).OrderBy(x => x.IngredientCategoryName).ThenBy(x => x.Name).ToListAsync();
        }

        public async Task UpdateAsync(int id, EditIngredientInputModel input)
        {
            var ingredient = await this.ingredientRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);
            ingredient.Name = input.Name;

            await this.ingredientRepository.SaveChangesAsync();
        }

        public async Task<ICollection<IngredientViewModel>> ShowAllDeletedAsync()
        {
            var allDough = await this.ingredientRepository.AllWithDeleted().Where(x => x.IsDeleted == true).ToListAsync();

            return allDough.Select(x => new IngredientViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }

        public async Task Delete(int id)
        {
            var ingredient = await this.ingredientRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.ingredientRepository.Delete(ingredient);
            await this.ingredientRepository.SaveChangesAsync();
        }

        public async Task Restore(int id)
        {
            var ingredient = await this.ingredientRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);
            this.ingredientRepository.Undelete(ingredient);
            await this.ingredientRepository.SaveChangesAsync();
        }
    }
}
