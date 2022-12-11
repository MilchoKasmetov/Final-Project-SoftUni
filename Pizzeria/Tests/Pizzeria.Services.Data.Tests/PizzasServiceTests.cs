namespace Pizzeria.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Pizzeria.Data.Models;
    using Pizzeria.Web.ViewModels.Pizzas;
    using Xunit;

    public class PizzasServiceTests : BaseServiceTests
    {
        private const int TestId = 1;
        private const int TestIdForSecoundExample = 2;
        private const decimal TestPrice = 69.69M;

        private const string TestName = "Test";
        private const string TestNameForSecoundExample = "Test two";
        private const string TestImageURLForPizzaExample = "https://foodhub.scene7.com/is/image/woolworthsltdprod/2004-easy-pepperoni-pizza:Mobile-1300x1150";
        private const string TestNameNull = null;
        private const string TestGuidUser = "2575bb3b-568c-4d6d-b0cb-555110b8eb52";

        private IPizzasService PizzasService => this.ServiceProvider.GetRequiredService<IPizzasService>();

        [Fact]
        public async Task CreatePizzaAsyncSuccessfully()
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
            var testNullName = new CreatePizzaInputModel()
            {
                Name = TestNameNull,
                ImageURL = TestImageURLForPizzaExample,
                DoughId = dough.Id,
                SauceDipId = sauce.Id,
                SizeId = size.Id,
                Price = TestPrice,
                Ingredients = ingredients,
            };

            await this.PizzasService.CreatePizzaAsync(testWithName, TestGuidUser);
            await this.PizzasService.CreatePizzaAsync(testWithName, TestGuidUser);
            await this.PizzasService.CreatePizzaAsync(testNullName, TestGuidUser);

            var list = await this.DbContext.Pizzas.ToListAsync();

            Assert.Equal(2, list.Count);
            Assert.Equal(TestName, list.FirstOrDefault(x => x.Name == TestName).Name);
            Assert.Equal(TestImageURLForPizzaExample, testWithName.ImageURL);
            Assert.Equal(dough.Id, testWithName.DoughId);
            Assert.Equal(sauce.Id, testWithName.SauceDipId);
            Assert.Equal(size.Id, testWithName.SizeId);
            Assert.Equal(ingredients.Length, testWithName.Ingredients.Length);
            Assert.Equal(ingredients[0].Name, testWithName.Ingredients[0].Name);
            Assert.Equal(ingredients[0].Id, testWithName.Ingredients[0].Id);
            Assert.Equal(ingredients[0].IngredientCategoryName, testWithName.Ingredients[0].IngredientCategoryName);
            Assert.True(testWithName.Ingredients[0].Selected);
        }

        [Fact]
        public async Task ShowAllPizzaAsyncSuccessfully()
        {
            var dough = await this.CreateTestDoughAsync();
            var sauce = await this.CreateTestSauceDipAsync();
            var size = await this.CreateTestSizeAsync();
            var ingredients = await this.CreateTestIngredientsAsync();

            var testWithName = new Pizza()
            {
                Name = TestName,
                ImageURL = TestImageURLForPizzaExample,
                DoughId = dough.Id,
                SauceDipId = sauce.Id,
                SizeId = size.Id,
                Price = TestPrice,
                Ingredients = ingredients,
            };
            await this.DbContext.Pizzas.AddAsync(testWithName);
            var testWithNameSecoundExample = new Pizza()
            {
                Name = TestNameForSecoundExample,
                ImageURL = TestImageURLForPizzaExample,
                DoughId = dough.Id,
                SauceDipId = sauce.Id,
                SizeId = size.Id,
                Price = TestPrice,
                Ingredients = ingredients,
            };

            await this.DbContext.Pizzas.AddAsync(testWithName);
            await this.DbContext.Pizzas.AddAsync(testWithNameSecoundExample);
            await this.DbContext.SaveChangesAsync();

            var list = await this.PizzasService.ShowAllPizzaAsync();

            Assert.Equal(2, list.Count);
        }

        [Fact]
        public async Task GetForUpdateAsyncSuccessfully()
        {
            var dough = await this.CreateTestDoughAsync();
            var sauce = await this.CreateTestSauceDipAsync();
            var size = await this.CreateTestSizeAsync();
            var ingredients = await this.CreateTestIngredientsAsync();

            var testWithName = new Pizza()
            {
                Name = TestName,
                ImageURL = TestImageURLForPizzaExample,
                DoughId = dough.Id,
                SauceDipId = sauce.Id,
                SizeId = size.Id,
                Price = TestPrice,
                Ingredients = ingredients,
            };
            await this.DbContext.Pizzas.AddAsync(testWithName);
            await this.DbContext.SaveChangesAsync();
            var output = await this.PizzasService.GetForUpdateAsync(TestId);

            Assert.Equal(TestName, output.Name);
        }

        [Fact]
        public async Task ShowAllDeletedAsyncSuccessfully()
        {
            var dough = await this.CreateTestDoughAsync();
            var sauce = await this.CreateTestSauceDipAsync();
            var size = await this.CreateTestSizeAsync();
            var ingredients = await this.CreateTestIngredientsAsync();

            var testWithName = new Pizza()
            {
                Name = TestName,
                ImageURL = TestImageURLForPizzaExample,
                DoughId = dough.Id,
                SauceDipId = sauce.Id,
                SizeId = size.Id,
                Price = TestPrice,
                Ingredients = ingredients,
                IsDeleted = false,
            };
            await this.DbContext.Pizzas.AddAsync(testWithName);
            var testWithNameSecoundExample = new Pizza()
            {
                Name = TestNameForSecoundExample,
                ImageURL = TestImageURLForPizzaExample,
                DoughId = dough.Id,
                SauceDipId = sauce.Id,
                SizeId = size.Id,
                Price = TestPrice,
                Ingredients = ingredients,
                IsDeleted = true,
            };

            await this.DbContext.Pizzas.AddAsync(testWithName);
            await this.DbContext.Pizzas.AddAsync(testWithNameSecoundExample);
            await this.DbContext.SaveChangesAsync();
            var output = await this.PizzasService.ShowAllDeletedAsync();

            Assert.Equal(1, output.Count);
            Assert.Equal(TestNameForSecoundExample, output.FirstOrDefault(x => x.Name == TestNameForSecoundExample).Name);
            Assert.Equal(2, output.FirstOrDefault(x => x.Id == TestIdForSecoundExample).Id);
        }

        [Fact]
        public async Task UpdateAsyncSuccessfully()
        {
            var dough = await this.CreateTestDoughAsync();
            var sauce = await this.CreateTestSauceDipAsync();
            var size = await this.CreateTestSizeAsync();
            var ingredients = await this.CreateTestIngredientsAsync();

            var ingredientsInput = new PizzaIngredientInputModel()
            {
                Id = ingredients[0].Id,
                Name = ingredients[0].Name,
                IngredientCategoryName = ingredients[0].IngredientCategory.Name,
                Selected = true,
            };

            var inputIngredientArr = new PizzaIngredientInputModel[1];
            inputIngredientArr[0] = ingredientsInput;

            var testWithName = new Pizza()
            {
                Name = TestName,
                ImageURL = TestImageURLForPizzaExample,
                DoughId = dough.Id,
                SauceDipId = sauce.Id,
                SizeId = size.Id,
                Price = TestPrice,
                Ingredients = ingredients,
            };
            await this.DbContext.Pizzas.AddAsync(testWithName);

            var testWithNameSecoundExample = new EditPizzaInputModel()
            {
                Name = TestNameForSecoundExample,
                ImageURL = TestImageURLForPizzaExample,
                DoughId = dough.Id,
                SauceDipId = sauce.Id,
                SizeId = size.Id,
                Price = TestPrice,
                Ingredients = inputIngredientArr,
            };

            await this.DbContext.Pizzas.AddAsync(testWithName);
            await this.DbContext.SaveChangesAsync();
            await this.PizzasService.UpdateAsync(TestId, testWithNameSecoundExample);

            Assert.Equal(TestNameForSecoundExample, testWithName.Name);
        }

        [Fact]
        public async Task DeleteSuccessfully()
        {
            var dough = await this.CreateTestDoughAsync();
            var sauce = await this.CreateTestSauceDipAsync();
            var size = await this.CreateTestSizeAsync();
            var ingredients = await this.CreateTestIngredientsAsync();

            var testWithName = new Pizza()
            {
                Name = TestName,
                ImageURL = TestImageURLForPizzaExample,
                DoughId = dough.Id,
                SauceDipId = sauce.Id,
                SizeId = size.Id,
                Price = TestPrice,
                Ingredients = ingredients,
                IsDeleted = false,
            };
            await this.DbContext.Pizzas.AddAsync(testWithName);
            await this.DbContext.SaveChangesAsync();
            await this.PizzasService.Delete(TestId);

            Assert.Equal(TestName, testWithName.Name);
            Assert.True(testWithName.IsDeleted);
        }

        [Fact]
        public async Task RestoreSuccessfully()
        {
            var dough = await this.CreateTestDoughAsync();
            var sauce = await this.CreateTestSauceDipAsync();
            var size = await this.CreateTestSizeAsync();
            var ingredients = await this.CreateTestIngredientsAsync();

            var testWithName = new Pizza()
            {
                Name = TestName,
                ImageURL = TestImageURLForPizzaExample,
                DoughId = dough.Id,
                SauceDipId = sauce.Id,
                SizeId = size.Id,
                Price = TestPrice,
                Ingredients = ingredients,
                IsDeleted = true,
            };
            await this.DbContext.Pizzas.AddAsync(testWithName);
            await this.DbContext.SaveChangesAsync();
            await this.PizzasService.Restore(TestId);

            Assert.Equal(TestName, testWithName.Name);
            Assert.False(testWithName.IsDeleted);
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

        public async Task<List<Ingredient>> CreateTestIngredientsAsync()
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

            var ingredietList = new List<Ingredient>();
            ingredietList.Add(testWithName);
            return ingredietList;
        }
    }
}
