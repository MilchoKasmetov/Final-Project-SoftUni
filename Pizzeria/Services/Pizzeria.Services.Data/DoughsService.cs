﻿namespace Pizzeria.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Pizzeria.Data.Common.Repositories;
    using Pizzeria.Data.Models;
    using Pizzeria.Web.ViewModels.Dough;
    using Pizzeria.Web.ViewModels.Pizzas;

    public class DoughsService : IDoughsService
    {
        private readonly IDeletableEntityRepository<Dough> doughRepository;

        public DoughsService(IDeletableEntityRepository<Dough> doughRepository)
        {
            this.doughRepository = doughRepository;
        }

        public async Task CreateDoughAsync(CreateDoughInputModel model)
        {
            var dough = new Dough()
            {
                Name = model.Name,
            };

            var allDought = await this.doughRepository.All().ToListAsync();

            if (!allDought.Any(x => x.Name == model.Name))
            {
                await this.doughRepository.AddAsync(dough);
                await this.doughRepository.SaveChangesAsync();
            }
        }

        public async Task<ICollection<PizzaDoughInputModel>> GetDoughsAsync()
        {
            return await this.doughRepository.AllAsNoTracking().Select(x => new PizzaDoughInputModel() { Id = x.Id, Name = x.Name }).ToListAsync();
        }
    }
}
