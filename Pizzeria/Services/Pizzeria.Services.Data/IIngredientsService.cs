namespace Pizzeria.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Pizzeria.Web.ViewModels.Ingredients;
    using Pizzeria.Web.ViewModels.Pizzas;

    public interface IIngredientsService
    {
        Task<ICollection<PizzaIngredientInputModel>> GetIngredientsAsync();

        Task<ICollection<IngredientViewModel>> GetAllIngredientsAsync();

        Task<ICollection<IngredientViewModel>> ShowAllDeletedAsync();

        Task CreateIngredientsAsync(CreateIngredientInputModel model);

        Task<EditIngredientInputModel> GetForUpdateAsync(int id);

        Task UpdateAsync(int id, EditIngredientInputModel input);

        Task Delete(int id);

        Task Restore(int id);
    }
}
