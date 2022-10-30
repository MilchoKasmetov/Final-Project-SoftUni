namespace Pizzeria.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Pizzeria.Data.Models;

    public interface IDoughsService
    {
        Task<ICollection<Dough>> GetDoughsAsync();
    }
}
