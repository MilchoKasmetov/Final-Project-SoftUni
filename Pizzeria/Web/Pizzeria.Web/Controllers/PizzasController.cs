namespace Pizzeria.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Pizzeria.Services.Data;
    using Pizzeria.Web.ViewModels.Pizzas;

    public class PizzasController : Controller
    {
        private readonly IDoughsService doughsService;
        private readonly ISauceDipsService sauceDipsService;


        //private readonly IPizzaService pizzaService;

        //public PizzasController(IPizzaService _pizzaService)
        //{
        //    pizzaService = _pizzaService;
        //}

        public PizzasController(IDoughsService doughsService, ISauceDipsService sauceDipsService)
        {
            this.doughsService = doughsService;
            this.sauceDipsService = sauceDipsService;
        }

     
        private string UserId => this.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public async Task<IActionResult> Create()
        {
            var model = new CreatePizzaInputModel();
            model.Doughs = await this.doughsService.GetDoughsAsync();
            model.SauceDips = await this.sauceDipsService.GetSauceDipsAsync();
            return this.View(model);
        }

        [HttpPost]
        public IActionResult Create(CreatePizzaInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            // da prenasochvam kam vsichki pizzi koito nai veroqtno shte sa na glavnata stranica
            return this.Redirect("/");
        }
    }
}
