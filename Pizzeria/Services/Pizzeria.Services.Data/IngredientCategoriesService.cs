using Microsoft.EntityFrameworkCore;
using Pizzeria.Data.Common.Repositories;
using Pizzeria.Data.Models;
using Pizzeria.Web.ViewModels.IngredientCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzeria.Services.Data
{
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
    }
}
