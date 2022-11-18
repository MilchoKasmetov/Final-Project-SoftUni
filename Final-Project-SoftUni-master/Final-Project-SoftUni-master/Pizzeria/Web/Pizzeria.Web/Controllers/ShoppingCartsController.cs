namespace Pizzeria.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Pizzeria.Data.Models;
    using Pizzeria.Services.Data;
    using Pizzeria.Web.ViewModels.ShoppingCart;

    public class ShoppingCartsController : BaseController
    {
        private readonly IShoppingCartsService shoppingCartsService;

        public ShoppingCartsController(
            IShoppingCartsService shoppingCartsService)
        {
            this.shoppingCartsService = shoppingCartsService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await this.shoppingCartsService.GetAll();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Buy(int id)
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.shoppingCartsService.Buy(id, userId);

            return this.RedirectToAction("All", "Pizzas");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.shoppingCartsService.Delete(id, userId);

            return this.View(nameof(this.Index));
        }

    }
}
