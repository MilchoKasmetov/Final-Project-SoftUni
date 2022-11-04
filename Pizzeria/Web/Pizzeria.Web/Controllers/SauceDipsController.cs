namespace Pizzeria.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Pizzeria.Services.Data;

    public class SauceDipsController : Controller
    {
        private readonly ISauceDipsService sauceDipsService;

        public SauceDipsController(ISauceDipsService sauceDipsService)
        {
            this.sauceDipsService = sauceDipsService;
        }

        public async Task<IActionResult> All()
        {
            var model = await this.sauceDipsService.GetAllSauceDipsAsync();
            return this.View(model);
        }
    }
}
