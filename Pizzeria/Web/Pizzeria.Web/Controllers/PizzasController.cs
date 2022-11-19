namespace Pizzeria.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Pizzeria.Data.Models;
    using Pizzeria.Services.Data;
    using Pizzeria.Web.ViewModels.Pizzas;

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
    }
}
