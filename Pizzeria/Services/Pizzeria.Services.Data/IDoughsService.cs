namespace Pizzeria.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Pizzeria.Web.ViewModels.Dough;
    using Pizzeria.Web.ViewModels.Pizzas;

    public interface IDoughsService
    {
        Task<ICollection<PizzaDoughInputModel>> GetDoughsAsync();

        Task<ICollection<DoughViewModel>> GetAllDoughsAsync();

        Task<ICollection<DoughViewModel>> ShowAllDeletedAsync();

        Task CreateDoughAsync(CreateDoughInputModel model);

        Task<EditDoughInputModel> GetForUpdateAsync(int id);

        Task UpdateAsync(int id, EditDoughInputModel input);

        Task Delete(int id);

        Task Restore(int id);
    }
}
