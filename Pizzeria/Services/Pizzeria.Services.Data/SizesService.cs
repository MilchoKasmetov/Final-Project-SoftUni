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

    public class SizesService : ISizesService
    {
        private readonly IDeletableEntityRepository<Size> sizeRepository;

        public SizesService(IDeletableEntityRepository<Size> sizeRepository)
        {
            this.sizeRepository = sizeRepository;
        }

        public async Task<ICollection<PizzaSizeInputModel>> GetSizesAsync()
        {
            return await this.sizeRepository.AllAsNoTracking().Select(x => new PizzaSizeInputModel() { Id = x.Id, Name = x.Name }).ToListAsync();
        }
    }
}
