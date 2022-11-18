namespace Pizzeria.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Pizzeria.Web.ViewModels.ShoppingCart;

    public interface IShoppingCartsService
    {
        Task Buy(int id, string userId);

        Task<ICollection<ShoppingCartViewModel>> GetAll();

        Task Delete(int id, string userId);
    }
}
