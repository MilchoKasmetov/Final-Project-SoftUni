using Microsoft.EntityFrameworkCore;
using Pizzeria.Data.Common.Repositories;
using Pizzeria.Data.Models;
using Pizzeria.Web.ViewModels.Pizzas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzeria.Services.Data
{
    public class ShoppingCartsService : IShoppingCartsService
    {
        private readonly IDeletableEntityRepository<ShoppingCart> shoppingCartRepository;
        private readonly IDeletableEntityRepository<Pizza> pizzaRepository;

        public ShoppingCartsService(
            IDeletableEntityRepository<ShoppingCart> shoppingCartRepository, 
            IDeletableEntityRepository<Pizza> pizzaRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.pizzaRepository = pizzaRepository;
        }

        public async Task Buy(int id, string userId)
        {
            var pizza = await this.pizzaRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == id);            
            var userCart = this.shoppingCartRepository.All().Include(x => x.ShoppingCartActivities).FirstOrDefault(x => x.User.Id == userId);

            var cardActivity = userCart.ShoppingCartActivities.FirstOrDefault(x => x.Id == id);

            if (!userCart.ShoppingCartActivities.Any(x => x.PizzaId == id))
            {
                cardActivity = new ShoppingCartActivity()
                {
                    PizzaId = pizza.Id,
                    Quantity = 1,
                };
                userCart.ShoppingCartActivities.Add(cardActivity);
            }
            else
            {
                userCart.ShoppingCartActivities.FirstOrDefault(x => x.PizzaId == id).Quantity += 1;
            }

            await this.shoppingCartRepository.SaveChangesAsync();
        }
    }
}
