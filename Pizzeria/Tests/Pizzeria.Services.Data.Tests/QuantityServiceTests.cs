using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pizzeria.Data.Models;
using Pizzeria.Web.ViewModels.Pizzas;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Pizzeria.Services.Data.Tests
{
    public class QuantityServiceTests : BaseServiceTests
    {
        private const decimal TestPrice = 69.69M;

        private const string TestName = "Test";
        private const string TestNameForEmail = "test@abv.bg";
        private const string TestImageURLForPizzaExample = "https://foodhub.scene7.com/is/image/woolworthsltdprod/2004-easy-pepperoni-pizza:Mobile-1300x1150";
        private const string TestGuidUser = "2575bb3b-568c-4d6d-b0cb-555110b8eb52";

        private IShoppingCartsService ShoppingCartsService => this.ServiceProvider.GetRequiredService<IShoppingCartsService>();
        private IQuantityService QuantityService => this.ServiceProvider.GetRequiredService<IQuantityService>();
        private IPizzasService PizzasService => this.ServiceProvider.GetRequiredService<IPizzasService>();

        [Fact]
        public async Task DecreaseSuccessfully()
        {
            await this.CreateTestPizza();
            await this.CreateTestUser();
            var testUser = await this.DbContext.Users.FirstAsync();
            await this.ShoppingCartsService.Buy(1, testUser.Id);
            await this.ShoppingCartsService.Buy(1, testUser.Id);
            await this.ShoppingCartsService.Buy(1, testUser.Id);
            await this.QuantityService.Decrease(1, testUser.Id);
            var allShoppingCarts = await this.ShoppingCartsService.GetAll(testUser.Id);

            var totalQuantity = allShoppingCarts.Select(x => x.Quantity).Sum(x => x);
            Assert.Equal(1, allShoppingCarts.Count);
            Assert.Equal(2, totalQuantity);
        }

        [Fact]
        public async Task IncreaseAllSuccessfully()
        {
            await this.CreateTestPizza();
            await this.CreateTestUser();
            var testUser = await this.DbContext.Users.FirstAsync();
            await this.ShoppingCartsService.Buy(1, testUser.Id);
            await this.QuantityService.Increase(1, testUser.Id);
            var allShoppingCarts = await this.ShoppingCartsService.GetAll(testUser.Id);

            var totalQuantity = allShoppingCarts.Select(x => x.Quantity).Sum(x => x);
            Assert.Equal(1, allShoppingCarts.Count);
            Assert.Equal(2, totalQuantity);
        }

        public async Task CreateTestUser()
        {
            var user = new ApplicationUser()
            {
                Email = TestNameForEmail,
                PasswordHash = TestNameForEmail,
            };

            await this.DbContext.AddAsync(user);
            await this.DbContext.SaveChangesAsync();
        }

        public async Task CreateTestPizza()
        {
            var dough = await this.CreateTestDoughAsync();
            var sauce = await this.CreateTestSauceDipAsync();
            var size = await this.CreateTestSizeAsync();
            var ingredients = await this.CreateTestIngredientsInputAsync();

            var testWithName = new CreatePizzaInputModel()
            {
                Name = TestName,
                ImageURL = TestImageURLForPizzaExample,
                DoughId = dough.Id,
                SauceDipId = sauce.Id,
                SizeId = size.Id,
                Price = TestPrice,
                Ingredients = ingredients,
            };

            await this.PizzasService.CreatePizzaAsync(testWithName, TestGuidUser);
        }

        public async Task<Dough> CreateTestDoughAsync()
        {
            var testWithName = new Dough()
            {
                Name = TestName,
            };

            await this.DbContext.Doughs.AddAsync(testWithName);
            await this.DbContext.SaveChangesAsync();

            return testWithName;
        }

        public async Task<SauceDip> CreateTestSauceDipAsync()
        {
            var testWithName = new SauceDip()
            {
                Name = TestName,
            };

            await this.DbContext.SauceDips.AddAsync(testWithName);
            await this.DbContext.SaveChangesAsync();

            return testWithName;
        }

        public async Task<Size> CreateTestSizeAsync()
        {
            var testWithName = new Size()
            {
                Name = TestName,
            };

            await this.DbContext.Sizes.AddAsync(testWithName);
            await this.DbContext.SaveChangesAsync();

            return testWithName;
        }

        public async Task<PizzaIngredientInputModel[]> CreateTestIngredientsInputAsync()
        {
            var testIngredientCat = new IngredientCategory()
            {
                Name = TestName,
            };
            await this.DbContext.IngredientCategories.AddAsync(testIngredientCat);

            var testWithName = new Ingredient()
            {
                Name = TestName,
                IngredientCategoryId = testIngredientCat.Id,
            };

            await this.DbContext.Ingredients.AddAsync(testWithName);
            await this.DbContext.SaveChangesAsync();

            var pizzaIngredientInputModel = new PizzaIngredientInputModel()
            {
                IngredientCategoryName = testIngredientCat.Name,
                Name = testWithName.Name,
                Selected = true,
            };

            var ingredietArray = new PizzaIngredientInputModel[1];
            ingredietArray[0] = pizzaIngredientInputModel;
            return ingredietArray;
        }
    }
}
