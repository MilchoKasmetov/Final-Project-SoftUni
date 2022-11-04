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
    }
}
