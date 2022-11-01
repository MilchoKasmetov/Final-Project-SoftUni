﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Services.Data;
using Pizzeria.Web.ViewModels.Dough;
using Pizzeria.Web.ViewModels.Pizzas;
using System.Threading.Tasks;

namespace Pizzeria.Web.Controllers
{
    public class DoughsController : Controller
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
            // da prenasochvam kam vsichki pizzi koito nai veroqtno shte sa na glavnata stranica
            return this.Redirect("/");
        }
    }
}
