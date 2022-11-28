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
        private readonly IDeletableEntityRepository<ShoppingCartActivity> shoppingCartActivity;

        public ShoppingCartsService(
            IDeletableEntityRepository<ShoppingCart> shoppingCartRepository,
            IDeletableEntityRepository<Pizza> pizzaRepository,
            IDeletableEntityRepository<ShoppingCartActivity> shoppingCartActivity)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.pizzaRepository = pizzaRepository;
            this.shoppingCartActivity = shoppingCartActivity;
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

        public async Task Delete(int id, string userId)
        {
            var shoppingCard = await this.shoppingCartRepository.All().Include(x => x.ShoppingCartActivities).FirstOrDefaultAsync(x => x.Id == id && x.User.Id == userId);

            this.shoppingCartActivity.HardDelete(shoppingCard.ShoppingCartActivities.FirstOrDefault());
            await this.shoppingCartRepository.SaveChangesAsync();
        }

        public async Task Delete(string userId)
        {
            var shoppingCard = await this.shoppingCartRepository.All().Include(x => x.ShoppingCartActivities).FirstOrDefaultAsync(x => x.User.Id == userId);

            this.shoppingCartActivity.HardDelete(shoppingCard.ShoppingCartActivities.FirstOrDefault());
            await this.shoppingCartRepository.SaveChangesAsync();
        }

        public async Task<ICollection<ShoppingCartViewModel>> GetAll(string userId)
        {
            var current = await this.shoppingCartRepository
                .All()
                .Include(x => x.ShoppingCartActivities)
                .ThenInclude(x => x.Pizza)
                .Include(x => x.ShoppingCartActivities)
                .ThenInclude(x => x.Pizza.Dough)
                .Include(x => x.ShoppingCartActivities)
                .ThenInclude(x => x.Pizza.Size)
                .Include(x => x.ShoppingCartActivities)
                .ThenInclude(x => x.Pizza.SauceDip)
                .Include(x => x.ShoppingCartActivities)
                .ThenInclude(x => x.Pizza.Ingredients)
                .Include(x => x.User)
                .Where(x => x.User.Id == userId)
                .FirstOrDefaultAsync();

            //if (all.Select(x => x.ShoppingCartActivities.Count).FirstOrDefault() == 0)
            //{
            //    throw new NullReferenceException();
            //}

                var model = current.ShoppingCartActivities.Select(x => new ShoppingCartViewModel()
                {
                    ShoppingCartActivityId = x.ShoppingCartId,
                    Name = x.Pizza.Name,
                    ImageURL = x.Pizza.ImageURL,
                    Dough = x.Pizza.Dough.Name,
                    SauceDip = x.Pizza.SauceDip.Name,
                    Ingredients = string.Join(", ", x.Pizza.Ingredients.Select(p => p.Name).FirstOrDefault()),
                    Size = x.Pizza.Size.Name,
                    Price = x.Pizza.Price,
                    Quantity = x.Quantity,
                    PizzaId = x.PizzaId,
                })
                .ToList();
                return model;
        }
    }
}
