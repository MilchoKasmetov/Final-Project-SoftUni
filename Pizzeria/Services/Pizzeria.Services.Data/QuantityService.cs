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

        public Task Decrease(int id, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task Increase(int ShoppingCartActivityId, string userId)
        {
            var shoppingCard = await this.shoppingCartRepository.All().Include(x => x.ShoppingCartActivities).FirstOrDefaultAsync(x => x.Id == ShoppingCartActivityId && x.User.Id == userId);

            var activity = shoppingCard.ShoppingCartActivities.FirstOrDefault();

            activity.Quantity += 1;

            await this.shoppingCartRepository.SaveChangesAsync();
        }
    }
}
