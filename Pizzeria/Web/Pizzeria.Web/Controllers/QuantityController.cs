namespace Pizzeria.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Pizzeria.Data.Common.Repositories;
    using Pizzeria.Data.Models;
    using Pizzeria.Services.Data;
    using Pizzeria.Web.ViewModels.Quantity;

    [ApiController]
    [Route("api/[controller]")]
    public class QuantityController : BaseController
    {
        private readonly IQuantityService quantityService;
        private readonly IRepository<ShoppingCartActivity> shoppingCardRepo;

        public QuantityController(
            IQuantityService quantityService,
            IRepository<ShoppingCartActivity> shoppingCardRepo
            )
        {

            this.quantityService = quantityService;
            this.shoppingCardRepo = shoppingCardRepo;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<QuantityResponseModel>> Post(InputQuantityViewModel input)
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.quantityService.Increase(input.ShoppingCartActivityId, userId);

            var shoppingCard = await this.shoppingCardRepo.All().Include(x => x.ShoppingCart.ShoppingCartActivities).Include(x => x.Pizza).FirstOrDefaultAsync(x => x.ShoppingCart.User.Id == userId);

            var totalPrice = shoppingCard.Quantity * shoppingCard.Pizza.Price;

            var response = new QuantityResponseModel()
            {
                TotalSum = totalPrice,
            };

            return response;
        }
    }
}
