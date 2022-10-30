namespace Pizzeria.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Pizzeria.Data.Models;
    using Pizzeria.Web.ViewModels.Pizzas;

    public interface IDoughsService
    {
        Task<ICollection<PizzaDoughInputModel>> GetDoughsAsync();
    }
}
