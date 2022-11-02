namespace Pizzeria.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Pizzeria.Data.Models;
    using Pizzeria.Services.Data;
    using Pizzeria.Web.ViewModels.ShoppingCart;

    public class ShoppingCartsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IShoppingCartsService shoppingCartsService;

        public ShoppingCartsController(
            UserManager<ApplicationUser> userManager,
            IShoppingCartsService shoppingCartsService)
        {
            this.userManager = userManager;
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
            var user = await this.userManager.GetUserAsync(this.User);
            await this.shoppingCartsService.Buy(id, user.Id);

            return this.RedirectToAction("All", "Pizzas");
        }
    }
}
