namespace Pizzeria.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Pizzeria.Web.ViewModels.Dough;
    using Pizzeria.Web.ViewModels.Ingredients;
    using Pizzeria.Web.ViewModels.Pizzas;

    public interface IIngredientsService
    {
        Task<ICollection<PizzaIngredientInputModel>> GetIngredientsAsync();

        Task<ICollection<IngredientViewModel>> GetAllIngredientsAsync();

        Task CreateIngredientsAsync(CreateIngredientInputModel model);
    }
}
