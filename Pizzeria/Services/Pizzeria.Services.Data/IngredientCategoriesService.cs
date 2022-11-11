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

    public class IngredientCategoriesService : IIngredientCategoriesService
    {
        private readonly IDeletableEntityRepository<IngredientCategory> ingredientCategoryRepository;

        public IngredientCategoriesService(IDeletableEntityRepository<IngredientCategory> ingredientCategoryRepository)
        {
            this.ingredientCategoryRepository = ingredientCategoryRepository;
        }

        public async Task<ICollection<IngredientCategoryViewModel>> GetIngredientCategoriesAsync()
        {
            return await this.ingredientCategoryRepository.All().Select(x => new IngredientCategoryViewModel() { Id = x.Id, Name = x.Name }).ToListAsync();
        }

        public async Task<ICollection<IngredientCategoryViewModel>> ShowAllDeletedAsync()
        {
            var allIngredientCategories = await this.ingredientCategoryRepository.AllWithDeleted().Where(x => x.IsDeleted == true).ToListAsync();

            return allIngredientCategories.Select(x => new IngredientCategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            })
            .ToList();
        }

        public async Task CreateIngredientCategoryAsync(CreateIngredientCategoriesInputModel model)
        {
            var category = new IngredientCategory()
            {
                Name = model.Name,
            };

            var allDought = await this.ingredientCategoryRepository.All().ToListAsync();

            if (!allDought.Any(x => x.Name == model.Name))
            {
                await this.ingredientCategoryRepository.AddAsync(category);
                await this.ingredientCategoryRepository.SaveChangesAsync();
            }
        }


        public async Task<EditIngredientCategoriesInputModel> GetForUpdateAsync(int id)
        {
            var category = await this.ingredientCategoryRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);
            var input = new EditIngredientCategoriesInputModel()
            {
                Name = category.Name,
            };

            return input;
        }

        public async Task UpdateAsync(int id, EditIngredientCategoriesInputModel input)
        {
            var dough = await this.ingredientCategoryRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);
            dough.Name = input.Name;

            await this.ingredientCategoryRepository.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var category = await this.ingredientCategoryRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.ingredientCategoryRepository.Delete(category);
            await this.ingredientCategoryRepository.SaveChangesAsync();
        }

        public async Task Restore(int id)
        {
            var dough = await this.ingredientCategoryRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);
            this.ingredientCategoryRepository.Undelete(dough);
            await this.ingredientCategoryRepository.SaveChangesAsync();
        }
    }
}
