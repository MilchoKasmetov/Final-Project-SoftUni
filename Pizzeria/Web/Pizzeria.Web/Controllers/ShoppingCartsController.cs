using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Data.Models;
using Pizzeria.Services.Data;
using System.Threading.Tasks;

namespace Pizzeria.Web.Controllers
{
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

        [HttpPost]
        public async Task<IActionResult> Buy(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.shoppingCartsService.Buy(id, user.Id);

            return this.RedirectToAction("All", "Pizzas");
        }
    }
}
