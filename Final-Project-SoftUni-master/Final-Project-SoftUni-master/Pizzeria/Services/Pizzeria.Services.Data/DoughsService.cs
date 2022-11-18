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

        public async Task<ICollection<DoughViewModel>> GetAllDoughsAsync()
        {
            return await this.doughRepository.All().Select(x => new DoughViewModel() { Id = x.Id, Name = x.Name }).ToListAsync();
        }

        public async Task<ICollection<PizzaDoughInputModel>> GetDoughsAsync()
        {
            return await this.doughRepository.AllAsNoTracking().Select(x => new PizzaDoughInputModel() { Id = x.Id, Name = x.Name }).ToListAsync();
        }

        public async Task<EditDoughInputModel> GetForUpdateAsync(int id)
        {
            var dough = await this.doughRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);
            var input = new EditDoughInputModel()
            {
                Name = dough.Name,
            };

            return input;
        }

        public async Task<ICollection<DoughViewModel>> ShowAllDeletedAsync()
        {
            var allDough = await this.doughRepository.AllWithDeleted().Where(x => x.IsDeleted == true).ToListAsync();

            return allDough.Select(x => new DoughViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }

        public async Task UpdateAsync(int id, EditDoughInputModel input)
        {
            var dough = await this.doughRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);
            dough.Name = input.Name;

            await this.doughRepository.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var dough = await this.doughRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.doughRepository.Delete(dough);
            await this.doughRepository.SaveChangesAsync();
        }

        public async Task Restore(int id)
        {
            var dough = await this.doughRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);
            this.doughRepository.Undelete(dough);
            await this.doughRepository.SaveChangesAsync();
        }
    }
}
