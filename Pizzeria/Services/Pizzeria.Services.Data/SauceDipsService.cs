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
    using Pizzeria.Web.ViewModels.Pizzas;
    using Pizzeria.Web.ViewModels.SauceDips;

    public class SauceDipsService : ISauceDipsService
    {
        private readonly IDeletableEntityRepository<SauceDip> sauceDipRepository;

        public SauceDipsService(IDeletableEntityRepository<SauceDip> sauceDipRepository)
        {
            this.sauceDipRepository = sauceDipRepository;
        }

        public async Task CreateDoughAsync(CreateSauceDipInputModel model)
        {
            var sauceDip = new SauceDip()
            {
                Name = model.Name,
            };

            var allSauceDip = await this.sauceDipRepository.All().ToListAsync();

            if (!allSauceDip.Any(x => x.Name == model.Name))
            {
                await this.sauceDipRepository.AddAsync(sauceDip);
                await this.sauceDipRepository.SaveChangesAsync();
            }
        }

        public async Task<ICollection<SauceDipViewModel>> GetAllSauceDipsAsync()
        {
            return await this.sauceDipRepository.All().Select(x => new SauceDipViewModel() { Id = x.Id, Name = x.Name }).ToListAsync();
        }

        public async Task<ICollection<PizzaSauceDipInputModel>> GetSauceDipsAsync()
        {
            return await this.sauceDipRepository.AllAsNoTracking().Select(x => new PizzaSauceDipInputModel() { Id = x.Id, Name = x.Name }).ToListAsync();
        }
    }
}
