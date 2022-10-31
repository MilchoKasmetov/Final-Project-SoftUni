namespace Pizzeria.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Pizzeria.Services.Data;
    using Pizzeria.Web.ViewModels.Pizzas;

    public class PizzasController : Controller
    {
        private readonly IDoughsService doughsService;
        private readonly ISauceDipsService sauceDipsService;
        private readonly IIngredientsService ingredientsService;
        private readonly ISizesService sizesService;
        private readonly IPizzasService pizzasService;


        //private readonly IPizzaService pizzaService;

        //public PizzasController(IPizzaService _pizzaService)
        //{
        //    pizzaService = _pizzaService;
        //}

        public PizzasController(
            IDoughsService doughsService,
            ISauceDipsService sauceDipsService,
            IIngredientsService ingredientsService,
            ISizesService sizesService,
            IPizzasService pizzasService)
        {
            this.doughsService = doughsService;
            this.sauceDipsService = sauceDipsService;
            this.ingredientsService = ingredientsService;
            this.sizesService = sizesService;
            this.pizzasService = pizzasService;
        }


        private string UserId => this.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public async Task<IActionResult> Create()
        {
            var model = new CreatePizzaInputModel();
            var ingredientsList = await this.ingredientsService.GetIngredientsAsync();
            model.Doughs = await this.doughsService.GetDoughsAsync();
            model.SauceDips = await this.sauceDipsService.GetSauceDipsAsync();
            model.Ingredients = ingredientsList.ToArray();
            model.Sizes = await this.sizesService.GetSizesAsync();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePizzaInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var ingredientsList = await this.ingredientsService.GetIngredientsAsync();
                input.Doughs = await this.doughsService.GetDoughsAsync();
                input.SauceDips = await this.sauceDipsService.GetSauceDipsAsync();
                input.Ingredients = ingredientsList.ToArray();
                input.Sizes = await this.sizesService.GetSizesAsync();
                return this.View();
            }
            return this.Json(input);

            await this.pizzasService.CreatePizzaAsync(input);
            // da prenasochvam kam vsichki pizzi koito nai veroqtno shte sa na glavnata stranica
            return this.Redirect("/");
        }
    }
}
