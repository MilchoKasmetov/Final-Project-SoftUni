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

    public class IngredientsService : IIngredientsService
    {
        private readonly IDeletableEntityRepository<Ingredient> ingredientRepository;

        public IngredientsService(IDeletableEntityRepository<Ingredient> ingredientRepository)
        {
            this.ingredientRepository = ingredientRepository;
        }

        public async Task<ICollection<PizzaIngredientInputModel>> GetIngredientsAsync()
        {
            return await this.ingredientRepository.AllAsNoTracking().Select(x => new PizzaIngredientInputModel() { Id = x.Id, Name = x.Name, IngredientCategoryName = x.IngredientCategory.Name }).OrderBy(x => x.IngredientCategoryName).ThenBy(x => x.Name).ToListAsync();
        }
    }
}
