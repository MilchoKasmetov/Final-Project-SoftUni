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

    public class PizzasController : Controller
    {
        private readonly IDoughsService doughsService;
        private readonly ISauceDipsService sauceDipsService;
        private readonly IIngredientsService ingredientsService;
        private readonly ISizesService sizesService;
        private readonly IPizzasService pizzasService;
        private readonly UserManager<ApplicationUser> userManager;

        public PizzasController(
            IDoughsService doughsService,
            ISauceDipsService sauceDipsService,
            IIngredientsService ingredientsService,
            ISizesService sizesService,
            IPizzasService pizzasService,
            UserManager<ApplicationUser> userManager)
        {
            this.doughsService = doughsService;
            this.sauceDipsService = sauceDipsService;
            this.ingredientsService = ingredientsService;
            this.sizesService = sizesService;
            this.pizzasService = pizzasService;
            this.userManager = userManager;
        }

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
        [Authorize]
        public async Task<IActionResult> Create(CreatePizzaInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.pizzasService.CreatePizzaAsync(input, user.Id);
            // da prenasochvam kam vsichki pizzi koito nai veroqtno shte sa na glavnata stranica
            return this.Redirect("/");
        }

        public async Task<IActionResult> All()
        {
            var model = await this.pizzasService.ShowAllPizzaAsync();

            return this.View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await this.pizzasService.GetForEditAsync(id);
            model.Doughs = await this.doughsService.GetDoughsAsync();
            model.SauceDips = await this.sauceDipsService.GetSauceDipsAsync();
            model.Sizes = await this.sizesService.GetSizesAsync();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditPizzaInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.pizzasService.UpdateAsync(id, input);

            return this.Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.pizzasService.Delete(id);

            return this.Redirect("/");
        }

        public async Task<IActionResult> Restore()
        {
            var model = await this.pizzasService.ShowAllDeletedPizzaAsync();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Restore(int id)
        {
            await this.pizzasService.Restore(id);

            return this.Redirect("/");
        }
    }
}
