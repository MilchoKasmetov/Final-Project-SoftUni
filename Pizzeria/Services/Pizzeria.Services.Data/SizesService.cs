namespace Pizzeria.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Pizzeria.Data.Common.Repositories;
    using Pizzeria.Web.ViewModels.Pizzas;
    using Pizzeria.Web.ViewModels.Sizes;

    using Size = Pizzeria.Data.Models.Size;

    public class SizesService : ISizesService
    {
        private readonly IDeletableEntityRepository<Size> sizeRepository;

        public SizesService(IDeletableEntityRepository<Size> sizeRepository)
        {
            this.sizeRepository = sizeRepository;
        }

        public async Task<ICollection<PizzaSizeInputModel>> GetSizeAsync()
        {
            return await this.sizeRepository.AllAsNoTracking().Select(x => new PizzaSizeInputModel() { Id = x.Id, Name = x.Name }).ToListAsync();
        }

        public async Task<ICollection<SizeViewModel>> GetAllSizesAsync()
        {
            return await this.sizeRepository.All().Select(x => new SizeViewModel() { Id = x.Id, Name = x.Name }).ToListAsync();
        }

        public async Task CreateSizeAsync(CreateSizeInputModel model)
        {
            var size = new Size()
            {
                Name = model.Name,
            };

            var allDought = await this.sizeRepository.All().ToListAsync();

            if (!allDought.Any(x => x.Name == model.Name) && model.Name != null)
            {
                await this.sizeRepository.AddAsync(size);
                await this.sizeRepository.SaveChangesAsync();
            }
        }

        public async Task<EditSizeInputModel> GetForUpdateAsync(int id)
        {
            var size = await this.sizeRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);
            var input = new EditSizeInputModel()
            {
                Name = size.Name,
            };

            return input;
        }

        public async Task<ICollection<SizeViewModel>> ShowAllDeletedAsync()
        {
            var allDough = await this.sizeRepository.AllWithDeleted().Where(x => x.IsDeleted == true).ToListAsync();

            return allDough.Select(x => new SizeViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }

        public async Task UpdateAsync(int id, EditSizeInputModel input)
        {
            var dough = await this.sizeRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);
            dough.Name = input.Name;

            await this.sizeRepository.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var size = await this.sizeRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.sizeRepository.Delete(size);
            await this.sizeRepository.SaveChangesAsync();
        }

        public async Task Restore(int id)
        {
            var size = await this.sizeRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);
            this.sizeRepository.Undelete(size);
            await this.sizeRepository.SaveChangesAsync();
        }
    }
}
