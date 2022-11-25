namespace Pizzeria.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Pizzeria.Services.Data;
    using Pizzeria.Web.Controllers;
    using Pizzeria.Web.ViewModels.Dough;
    using Pizzeria.Web.ViewModels.IngredientCategories;

    public class IngredientCategoriesController : AdministrationController
    {
        private readonly IIngredientCategoriesService ingredientCategoriesService;

        public IngredientCategoriesController(IIngredientCategoriesService ingredientCategoriesService)
        {
            this.ingredientCategoriesService = ingredientCategoriesService;
        }

        public async Task<IActionResult> All()
        {
            var model = await this.ingredientCategoriesService.GetIngredientCategoriesAsync();
            return this.View(model);
        }

        public async Task<IActionResult> Restore()
        {
            var model = await this.ingredientCategoriesService.ShowAllDeletedAsync();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Restore(int id)
        {
            await this.ingredientCategoriesService.Restore(id);

            return this.RedirectToAction("Restore", "IngredientCategories");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.ingredientCategoriesService.Delete(id);

            return this.RedirectToAction("All", "IngredientCategories");
        }

        public IActionResult Create()
        {
            var model = new CreateIngredientCategoriesInputModel();

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateIngredientCategoriesInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.ingredientCategoriesService.CreateIngredientCategoryAsync(input);
            return this.RedirectToAction("All", "IngredientCategories");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await this.ingredientCategoriesService.GetForUpdateAsync(id);

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditIngredientCategoriesInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.ingredientCategoriesService.UpdateAsync(id, input);
            return this.RedirectToAction("All", "IngredientCategories");
        }
    }
}
