using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Services.Data;
using Pizzeria.Web.ViewModels.Dough;
using Pizzeria.Web.ViewModels.Ingredients;
using System.Threading.Tasks;

namespace Pizzeria.Web.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly IIngredientsService ingredientsService;
        private readonly IIngredientCategoriesService ingredientCategoriesService;

        public IngredientsController(
            IIngredientsService ingredientsService,
            IIngredientCategoriesService ingredientCategoriesService)
        {
            this.ingredientsService = ingredientsService;
            this.ingredientCategoriesService = ingredientCategoriesService;
        }

        public async Task<IActionResult> All()
        {
            var model = await this.ingredientsService.GetAllIngredientsAsync();
            return this.View(model);
        }


        public async Task<IActionResult> Create()
        {
            var model = new CreateIngredientInputModel();
            model.IngredientCategories = await this.ingredientCategoriesService.GetIngredientCategoriesAsync();

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateIngredientInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.ingredientsService.CreateIngredientsAsync(input);
            // da prenasochvam kam vsichki pizzi koito nai veroqtno shte sa na glavnata stranica
            return this.RedirectToAction("All", "Ingredients");
        }


        public async Task<IActionResult> Edit(int id)
        {
            var model = await this.ingredientsService.GetForUpdateAsync(id);
            model.IngredientCategories = await this.ingredientCategoriesService.GetIngredientCategoriesAsync();

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditIngredientInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.ingredientsService.UpdateAsync(id, input);
            // da prenasochvam kam vsichki pizzi koito nai veroqtno shte sa na glavnata stranica
            return this.RedirectToAction("All", "Ingredients");
        }
    }
}
