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
    using Pizzeria.Web.ViewModels.ShoppingCart;

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

        public async Task<ICollection<ShoppingCartViewModel>> GetAll()
        {
            var all = await this.shoppingCartRepository
                .All()
                .Include(x => x.ShoppingCartActivities)
                .ThenInclude(x => x.Pizza)
                .Include(x => x.ShoppingCartActivities)
                .ThenInclude( x => x.Pizza.Dough)
                .Include(x => x.ShoppingCartActivities)
                .ThenInclude(x => x.Pizza.Size)
                .Include(x => x.ShoppingCartActivities)
                .ThenInclude(x => x.Pizza.SauceDip)
                .Include(x => x.ShoppingCartActivities)
                .ThenInclude(x => x.Pizza.Ingredients)
                .ToListAsync();

            var model = all.Select(x => new ShoppingCartViewModel()
            {
                Name = x.ShoppingCartActivities.Select(x => x.Pizza.Name).FirstOrDefault().ToString(),
                ImageURL = x.ShoppingCartActivities.Select(p => p.Pizza.ImageURL).FirstOrDefault().ToString(),
                Dough = x.ShoppingCartActivities.Select(p => p.Pizza.Dough.Name).FirstOrDefault().ToString(),
                SauceDip = x.ShoppingCartActivities.Select(p => p.Pizza.SauceDip.Name).FirstOrDefault().ToString(),
                Ingredients = string.Join(", ", x.ShoppingCartActivities.Select(p => p.Pizza.Ingredients.Select(r => r.Name)).FirstOrDefault()),
                Size = x.ShoppingCartActivities.Select(p => p.Pizza.Size.Name).FirstOrDefault().ToString(),
                Price = x.ShoppingCartActivities.Select(p => p.Pizza.Price).FirstOrDefault(),
                Quantity = x.ShoppingCartActivities.Select(p => p.Quantity).FirstOrDefault(),

            }).ToList();
            return model;
        }
    }
}
