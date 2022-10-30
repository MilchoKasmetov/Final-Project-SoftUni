namespace Pizzeria.Services.Data
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

    public class SauceDipsService : ISauceDipsService
    {
        private readonly IDeletableEntityRepository<SauceDip> sauceDipRepository;

        public SauceDipsService(IDeletableEntityRepository<SauceDip> sauceDipRepository)
        {
            this.sauceDipRepository = sauceDipRepository;
        }

        public async Task<ICollection<PizzaSauceDipInputModel>> GetSauceDipsAsync()
        {
            return await this.sauceDipRepository.AllAsNoTracking().Select(x => new PizzaSauceDipInputModel() { Id = x.Id, Name = x.Name }).ToListAsync();
        }
    }
}
