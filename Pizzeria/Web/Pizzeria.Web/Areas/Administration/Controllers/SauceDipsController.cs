namespace Pizzeria.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Pizzeria.Services.Data;
    using Pizzeria.Web.ViewModels.SauceDips;

    public class SauceDipsController : AdministrationController
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

        public IActionResult Create()
        {
            var model = new CreateSauceDipInputModel();

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateSauceDipInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.sauceDipsService.CreateSauceDipAsync(input);
            // da prenasochvam kam vsichki pizzi koito nai veroqtno shte sa na glavnata stranica
            return this.RedirectToAction("All", "SauceDips");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await this.sauceDipsService.GetForUpdateAsync(id);

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditSauceDipInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.sauceDipsService.UpdateAsync(id, input);
            // da prenasochvam kam vsichki pizzi koito nai veroqtno shte sa na glavnata stranica
            return this.RedirectToAction("All", "SauceDips");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.sauceDipsService.Delete(id);

            return this.RedirectToAction("All", "SauceDips");
        }

        public async Task<IActionResult> Restore()
        {
            var model = await this.sauceDipsService.ShowAllDeletedAsync();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Restore(int id)
        {
            await this.sauceDipsService.Restore(id);

            return this.RedirectToAction("Restore", "SauceDips");
        }
    }
}
