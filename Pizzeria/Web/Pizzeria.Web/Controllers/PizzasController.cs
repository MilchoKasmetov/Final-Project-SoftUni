namespace Pizzeria.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Pizzeria.Services.Data;

    public class PizzasController : BaseController
    {
        private readonly IPizzasService pizzasService;

        public PizzasController(IPizzasService pizzasService)
        {
            this.pizzasService = pizzasService;
        }

        public async Task<IActionResult> All()
        {
            var model = await this.pizzasService.ShowAllPizzaAsync();

            return this.View(model);
        }

        public async Task<IActionResult> Index()
        {
            var model = await this.pizzasService.ShowAllPizzaAsync();

            return this.View(model);
        }
    }
}
