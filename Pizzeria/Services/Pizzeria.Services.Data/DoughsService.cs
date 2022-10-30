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

    public class DoughsService : IDoughsService
    {
        private readonly IDeletableEntityRepository<Dough> doughRepository;

        public DoughsService(IDeletableEntityRepository<Dough> doughRepository)
        {
            this.doughRepository = doughRepository;
        }

        public async Task<ICollection<Dough>> GetDoughsAsync()
        {
          return await this.doughRepository.All().ToListAsync();
        }
    }
}
