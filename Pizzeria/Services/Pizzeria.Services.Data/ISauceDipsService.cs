namespace Pizzeria.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Pizzeria.Web.ViewModels.Pizzas;
    using Pizzeria.Web.ViewModels.SauceDips;

    public interface ISauceDipsService
    {
        Task<ICollection<PizzaSauceDipInputModel>> GetSauceDipsAsync();

        Task<ICollection<SauceDipViewModel>> GetAllSauceDipsAsync();

        Task<ICollection<SauceDipViewModel>> ShowAllDeletedAsync();

        Task CreateSauceDipAsync(CreateSauceDipInputModel model);

        Task<EditSauceDipInputModel> GetForUpdateAsync(int id);

        Task UpdateAsync(int id, EditSauceDipInputModel input);

        Task Delete(int id);

        Task Restore(int id);
    }
}
