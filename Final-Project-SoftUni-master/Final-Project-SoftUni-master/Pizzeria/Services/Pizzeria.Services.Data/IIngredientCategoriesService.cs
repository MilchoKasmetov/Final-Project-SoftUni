namespace Pizzeria.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Pizzeria.Web.ViewModels.IngredientCategories;
    using Pizzeria.Web.ViewModels.Pizzas;

    public interface IIngredientCategoriesService
    {
        Task<ICollection<IngredientCategoryViewModel>> GetIngredientCategoriesAsync();

        Task<ICollection<IngredientCategoryViewModel>> ShowAllDeletedAsync();

        Task CreateIngredientCategoryAsync(CreateIngredientCategoriesInputModel model);

        Task<EditIngredientCategoriesInputModel> GetForUpdateAsync(int id);

        Task UpdateAsync(int id, EditIngredientCategoriesInputModel input);

        Task Delete(int id);

        Task Restore(int id);
    }
}
