namespace Pizzeria.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Pizzeria.Web.ViewModels.Pizzas;

    public interface IPizzasService
    {
        Task CreatePizzaAsync(CreatePizzaInputModel model, string userId);

        Task<ICollection<PizzaViewModel>> ShowAllPizzaAsync();

        Task<ICollection<PizzaViewModel>> ShowAllDeletedPizzaAsync();

        Task<EditPizzaInputModel> GetForUpdateAsync(int id);

        Task UpdateAsync(int id, EditPizzaInputModel input);

        Task Delete(int id);

        Task Restore(int id);
    }
}
