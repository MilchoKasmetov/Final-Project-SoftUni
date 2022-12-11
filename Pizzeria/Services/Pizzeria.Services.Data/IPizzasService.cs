namespace Pizzeria.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Pizzeria.Web.ViewModels.Pizzas;

    public interface IPizzasService
    {
        Task CreatePizzaAsync(CreatePizzaInputModel model, string userId);

        Task<ICollection<PizzaViewModel>> ShowAllPizzaAsync();

        Task<ICollection<PizzaViewModel>> ShowAllDeletedAsync();

        Task<EditPizzaInputModel> GetForUpdateAsync(int id);

        Task UpdateAsync(int id, EditPizzaInputModel input);

        Task Delete(int id);

        Task Restore(int id);
    }
}
