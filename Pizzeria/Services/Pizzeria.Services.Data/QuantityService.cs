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

    public class QuantityService : IQuantityService
    {
        private readonly IDeletableEntityRepository<ShoppingCart> shoppingCartRepository;

        public QuantityService(IDeletableEntityRepository<ShoppingCart> shoppingCartRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public async Task Decrease(int id, string userId)
        {
            var shoppingCard = await this.shoppingCartRepository.All().Include(x => x.ShoppingCartActivities).FirstOrDefaultAsync(x => x.Id == id && x.User.Id == userId);

            var activity = shoppingCard.ShoppingCartActivities.FirstOrDefault();

            activity.Quantity -= 1;

            await this.shoppingCartRepository.SaveChangesAsync();
        }

        public async Task Increase(int id, string userId)
        {
            var shoppingCard = await this.shoppingCartRepository.All().Include(x => x.ShoppingCartActivities).FirstOrDefaultAsync(x => x.Id == id && x.User.Id == userId);

            var activity = shoppingCard.ShoppingCartActivities.FirstOrDefault();

            activity.Quantity += 1;

            await this.shoppingCartRepository.SaveChangesAsync();
        }
    }
}
