namespace Pizzeria.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Pizzeria.Services.Data;
    using Pizzeria.Web.ViewModels.Dough;
    using Pizzeria.Web.ViewModels.SauceDips;

    public class SauceDipsController : Controller
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

            await this.sauceDipsService.CreateDoughAsync(input);
            // da prenasochvam kam vsichki pizzi koito nai veroqtno shte sa na glavnata stranica
            return this.RedirectToAction("All", "SauceDips");
        }
    }
}
