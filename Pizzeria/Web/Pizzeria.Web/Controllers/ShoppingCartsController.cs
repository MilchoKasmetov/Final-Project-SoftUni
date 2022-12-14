namespace Pizzeria.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Pizzeria.Common;
    using Pizzeria.Services.Data;
    using Stripe;

    [Authorize(Roles = GlobalConstants.UserRoleName)]
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
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = await this.shoppingCartsService.GetAll(userId);

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

            return this.RedirectToAction("Index", "ShoppingCarts");
        }

        // Stripe
        public async Task<IActionResult> Charge(string stripeEmail, string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken,
            });
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmail = this.User.FindFirstValue(ClaimTypes.Email);
            string userAddress = this.User.FindFirstValue("Address");
            var allProducts = await this.shoppingCartsService.GetAll(userId);
            var quantity = allProducts.Select(x => x.Quantity).Sum();
            var totalPrice = allProducts.Select(x => x.TotalPrice).FirstOrDefault() * 100;

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = (long)totalPrice,
                Description = $"{userEmail} bought {allProducts.Count * quantity} of pizzas on {DateTime.UtcNow} - need to be deliver {userAddress}",
                Currency = "usd",
                Customer = customer.Id,
                ReceiptEmail = stripeEmail,

            });

            if (charge.Status != "succeeded")
            {
                //optional - string balanceTransactionId = charge.BalanceTransactionId;
                return this.View("Error");
            }

            await this.shoppingCartsService.Delete(userId);

            return this.View("_OrderConfirmation");
        }
    }
}
