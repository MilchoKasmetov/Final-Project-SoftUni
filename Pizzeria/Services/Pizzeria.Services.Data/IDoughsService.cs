namespace Pizzeria.Services.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Pizzeria.Data.Models;
    using Pizzeria.Web.ViewModels.Dough;
    using Pizzeria.Web.ViewModels.Pizzas;

    public interface IDoughsService
    {
        Task<ICollection<PizzaDoughInputModel>> GetDoughsAsync();

        Task<ICollection<DoughViewModel>> GetAllDoughsAsync();

        Task<ICollection<DoughViewModel>> ShowAllDeletedDoughsAsync();

        Task CreateDoughAsync(CreateDoughInputModel model);

        Task<EditDoughInputModel> GetForEditAsync(int id);

        Task UpdateAsync(int id, EditDoughInputModel input);

        Task Delete(int id);

        Task Restore(int id);
    }
}
