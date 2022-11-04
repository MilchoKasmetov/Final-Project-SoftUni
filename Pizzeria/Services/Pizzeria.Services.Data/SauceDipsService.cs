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

        public async Task<EditSauceDipInputModel> GetForUpdateAsync(int id)
        {
            var sauceDip = await this.sauceDipRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);
            var input = new EditSauceDipInputModel()
            {
                Name = sauceDip.Name,
            };

            return input;
        }

        public async Task<ICollection<PizzaSauceDipInputModel>> GetSauceDipsAsync()
        {
            return await this.sauceDipRepository.AllAsNoTracking().Select(x => new PizzaSauceDipInputModel() { Id = x.Id, Name = x.Name }).ToListAsync();
        }

        public async Task UpdateAsync(int id, EditSauceDipInputModel input)
        {
            var sauceDip = await this.sauceDipRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);
            sauceDip.Name = input.Name;

            await this.sauceDipRepository.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var sauceDip = await this.sauceDipRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.sauceDipRepository.Delete(sauceDip);
            await this.sauceDipRepository.SaveChangesAsync();
        }

        public async Task Restore(int id)
        {
            var sauceDip = await this.sauceDipRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);
            this.sauceDipRepository.Undelete(sauceDip);
            await this.sauceDipRepository.SaveChangesAsync();
        }

        public async Task<ICollection<SauceDipViewModel>> ShowAllDeletedAsync()
        {
            var allSauceDip = await this.sauceDipRepository.AllWithDeleted().Where(x => x.IsDeleted == true).ToListAsync();

            return allSauceDip.Select(x => new SauceDipViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }
    }
}
