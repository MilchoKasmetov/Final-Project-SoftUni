namespace Pizzeria.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Pizzeria.Services.Data;
    using Pizzeria.Web.Controllers;
    using Pizzeria.Web.ViewModels.Dough;
    using Pizzeria.Web.ViewModels.Sizes;

    public class SizesController : AdministrationController
    {
        private readonly ISizesService sizesService;

        public SizesController(ISizesService sizesService)
        {
            this.sizesService = sizesService;
        }

        public IActionResult Create()
        {
            var model = new CreateSizeInputModel();

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateSizeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.sizesService.CreateSizeAsync(input);
            return this.RedirectToAction("All", "Sizes");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await this.sizesService.GetForUpdateAsync(id);

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditSizeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.sizesService.UpdateAsync(id, input);
            return this.RedirectToAction("All", "Sizes");
        }

        public async Task<IActionResult> All()
        {
            var model = await this.sizesService.GetAllSizesAsync();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.sizesService.Delete(id);

            return this.RedirectToAction("All", "Sizes");
        }

        public async Task<IActionResult> Restore()
        {
            var model = await this.sizesService.ShowAllDeletedAsync();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Restore(int id)
        {
            await this.sizesService.Restore(id);

            return this.RedirectToAction("Restore", "Sizes");
        }
    }
}
