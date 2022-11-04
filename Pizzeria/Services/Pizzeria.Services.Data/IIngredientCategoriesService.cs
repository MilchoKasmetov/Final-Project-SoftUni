using Pizzeria.Web.ViewModels.IngredientCategories;
using Pizzeria.Web.ViewModels.Pizzas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzeria.Services.Data
{
    public interface IIngredientCategoriesService
    {
        Task<ICollection<IngredientCategoryViewModel>> GetIngredientCategoriesAsync();
    }
}
