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
    }
}
