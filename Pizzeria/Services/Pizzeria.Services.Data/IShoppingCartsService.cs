namespace Pizzeria.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Pizzeria.Web.ViewModels.ShoppingCart;

    public interface IShoppingCartsService
    {
        Task Buy(int id, string userId);

        Task<ICollection<ShoppingCartViewModel>> GetAll(string userId);

        Task Delete(int id, string userId);

        Task Delete(string userId);
    }
}
