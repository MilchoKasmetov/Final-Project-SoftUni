namespace Pizzeria.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Pizzeria.Data.Models;
    using Pizzeria.Web.ViewModels.Dough;
    using Pizzeria.Web.ViewModels.Pizzas;

    public interface IDoughsService
    {
        Task<ICollection<PizzaDoughInputModel>> GetDoughsAsync();

        Task CreateDoughAsync(CreateDoughInputModel model);

        Task<EditDoughInputModel> GetForEditAsync(int id);

        Task UpdateAsync(int id, EditDoughInputModel input);
    }
}
