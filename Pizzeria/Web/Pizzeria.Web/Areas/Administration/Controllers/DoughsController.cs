namespace Pizzeria.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Pizzeria.Services.Data;
    using Pizzeria.Web.Controllers;
    using Pizzeria.Web.ViewModels.Dough;
    using Pizzeria.Web.ViewModels.Pizzas;

    public class DoughsController : AdministrationController
    {
        private readonly IDoughsService doughsService;

        public DoughsController(IDoughsService doughsService)
        {
            this.doughsService = doughsService;
        }

        public IActionResult Create()
        {
            var model = new CreateDoughInputModel();

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateDoughInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.doughsService.CreateDoughAsync(input);
            return this.RedirectToAction("All", "Doughs");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await this.doughsService.GetForUpdateAsync(id);

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditDoughInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.doughsService.UpdateAsync(id, input);
            return this.RedirectToAction("All", "Doughs");
        }

        public async Task<IActionResult> All()
        {
            var model = await this.doughsService.GetAllDoughsAsync();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.doughsService.Delete(id);

            return this.RedirectToAction("All", "Doughs");
        }

        public async Task<IActionResult> Restore()
        {
            var model = await this.doughsService.ShowAllDeletedAsync();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Restore(int id)
        {
            await this.doughsService.Restore(id);

            return this.RedirectToAction("Restore", "Doughs");
        }
    }
}
