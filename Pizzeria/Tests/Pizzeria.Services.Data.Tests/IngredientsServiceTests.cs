namespace Pizzeria.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Pizzeria.Data.Models;
    using Pizzeria.Web.ViewModels.IngredientCategories;
    using Pizzeria.Web.ViewModels.Ingredients;
    using Xunit;

    public class IngredientsServiceTests : BaseServiceTests
    {
        private const int TestId = 1;
        private const int TestIdForSecoundExample = 2;

        private const string TestName = "Test";
        private const string TestNameForSecoundExample = "Test two";
        private const string TestNameNull = null;

        private IIngredientsService IngredientsService => this.ServiceProvider.GetRequiredService<IIngredientsService>();
        //because of tight deadline there is some repeating logic that might be optimised Arrange stage near the future.
        [Fact]
        public async Task CreateIngredientsAsyncSuccessfully()
        {
            var testWithIngredientCategory = new IngredientCategory()
            {
                Name = TestName,
            };

            await this.DbContext.IngredientCategories.AddAsync(testWithIngredientCategory);
            await this.DbContext.SaveChangesAsync();


            var testWithName = new CreateIngredientInputModel()
            {
                Name = TestName,
                IngredientCategoryId = testWithIngredientCategory.Id,
            };
            var testNullName = new CreateIngredientInputModel()
            {
                Name = TestNameNull,
                IngredientCategoryId = testWithIngredientCategory.Id,
            };

            await this.IngredientsService.CreateIngredientsAsync(testWithName);
            await this.IngredientsService.CreateIngredientsAsync(testWithName);
            await this.IngredientsService.CreateIngredientsAsync(testNullName);

            var list = await this.DbContext.IngredientCategories.ToListAsync();

            Assert.Single(list);
            Assert.Equal(TestName, list.FirstOrDefault(x => x.Name == TestName).Name);
            Assert.Equal(testWithName.IngredientCategoryId, testWithIngredientCategory.Id);

        }

        [Fact]
        public async Task GetAllIngredientsAsync()
        {
            var testWithIngredientCategory = new IngredientCategory()
            {
                Name = TestName,
            };

            await this.DbContext.IngredientCategories.AddAsync(testWithIngredientCategory);
            await this.DbContext.SaveChangesAsync();

            var testWithName = new Ingredient()
            {
                Name = TestName,
                IsDeleted = false,
                IngredientCategoryId = testWithIngredientCategory.Id,
            };
            await this.DbContext.Ingredients.AddAsync(testWithName);
            var testWithNameSecoundExample = new Ingredient()
            {
                Name = TestNameForSecoundExample,
                IsDeleted = false,
                IngredientCategoryId = testWithIngredientCategory.Id,
            };

            await this.DbContext.Ingredients.AddAsync(testWithName);
            await this.DbContext.Ingredients.AddAsync(testWithNameSecoundExample);
            await this.DbContext.SaveChangesAsync();

            var list = await this.IngredientsService.GetAllIngredientsAsync();

            Assert.Equal(2, list.Count);
        }

        [Fact]
        public async Task GetForUpdateAsyncSuccessfully()
        {

            var testWithIngredientCategory = new IngredientCategory()
            {
                Name = TestName,
            };

            await this.DbContext.IngredientCategories.AddAsync(testWithIngredientCategory);
            await this.DbContext.SaveChangesAsync();

            var testWithName = new Ingredient()
            {
                Name = TestName,
                IsDeleted = false,
                IngredientCategoryId = testWithIngredientCategory.Id,
            };
            await this.DbContext.Ingredients.AddAsync(testWithName);
            await this.DbContext.SaveChangesAsync();
            var output = await this.IngredientsService.GetForUpdateAsync(TestId);

            Assert.Equal(TestName, output.Name);
        }

        [Fact]
        public async Task ShowAllDeletedAsyncSuccessfully()
        {
            var testWithIngredientCategory = new IngredientCategory()
            {
                Name = TestName,
            };

            await this.DbContext.IngredientCategories.AddAsync(testWithIngredientCategory);
            await this.DbContext.SaveChangesAsync();

            var testWithName = new Ingredient()
            {
                Name = TestName,
                IsDeleted = false,
                IngredientCategoryId = testWithIngredientCategory.Id,
            };
            await this.DbContext.Ingredients.AddAsync(testWithName);
            var testWithNameSecoundExample = new Ingredient()
            {
                Name = TestNameForSecoundExample,
                IsDeleted = true,
                IngredientCategoryId = testWithIngredientCategory.Id,
            };

            await this.DbContext.Ingredients.AddAsync(testWithName);
            await this.DbContext.Ingredients.AddAsync(testWithNameSecoundExample);
            await this.DbContext.SaveChangesAsync();
            var output = await this.IngredientsService.ShowAllDeletedAsync();

            Assert.Equal(1, output.Count);
            Assert.Equal(TestNameForSecoundExample, output.FirstOrDefault(x => x.Name == TestNameForSecoundExample).Name);
            Assert.Equal(2, output.FirstOrDefault(x => x.Id == TestIdForSecoundExample).Id);
        }

        [Fact]
        public async Task UpdateAsyncSuccessfully()
        {
            var testWithIngredientCategory = new IngredientCategory()
            {
                Name = TestName,
            };

            await this.DbContext.IngredientCategories.AddAsync(testWithIngredientCategory);
            await this.DbContext.SaveChangesAsync();

            var testWithName = new Ingredient()
            {
                Name = TestName,
                IsDeleted = false,
                IngredientCategoryId = testWithIngredientCategory.Id,
            };
            await this.DbContext.Ingredients.AddAsync(testWithName);

            var testWithNameSecoundExample = new EditIngredientInputModel()
            {
                Name = TestNameForSecoundExample,
            };

            await this.DbContext.Ingredients.AddAsync(testWithName);
            await this.DbContext.SaveChangesAsync();
            await this.IngredientsService.UpdateAsync(TestId, testWithNameSecoundExample);

            Assert.Equal(TestNameForSecoundExample, testWithName.Name);
        }

        [Fact]
        public async Task DeleteSuccessfully()
        {
            var testWithIngredientCategory = new IngredientCategory()
            {
                Name = TestName,
            };

            await this.DbContext.IngredientCategories.AddAsync(testWithIngredientCategory);
            await this.DbContext.SaveChangesAsync();

            var testWithName = new Ingredient()
            {
                Name = TestName,
                IsDeleted = false,
                IngredientCategoryId = testWithIngredientCategory.Id,
            };
            await this.DbContext.Ingredients.AddAsync(testWithName);
            await this.DbContext.SaveChangesAsync();
            await this.IngredientsService.Delete(TestId);

            Assert.Equal(TestName, testWithName.Name);
            Assert.True(testWithName.IsDeleted);
        }

        [Fact]
        public async Task RestoreSuccessfully()
        {
            var testWithIngredientCategory = new IngredientCategory()
            {
                Name = TestName,
            };

            await this.DbContext.IngredientCategories.AddAsync(testWithIngredientCategory);
            await this.DbContext.SaveChangesAsync();

            var testWithName = new Ingredient()
            {
                Name = TestName,
                IsDeleted = false,
                IngredientCategoryId = testWithIngredientCategory.Id,
            };
            await this.DbContext.Ingredients.AddAsync(testWithName);
            await this.DbContext.SaveChangesAsync();
            await this.IngredientsService.Restore(TestId);

            Assert.Equal(TestName, testWithName.Name);
            Assert.False(testWithName.IsDeleted);
        }

        [Fact]
        public async Task GetIngredientsAsync()
        {
            var testWithIngredientCategory = new IngredientCategory()
            {
                Name = TestName,
            };

            await this.DbContext.IngredientCategories.AddAsync(testWithIngredientCategory);
            await this.DbContext.SaveChangesAsync();

            var testWithName = new Ingredient()
            {
                Name = TestName,
                IsDeleted = false,
                IngredientCategoryId = testWithIngredientCategory.Id,
            };
            await this.DbContext.Ingredients.AddAsync(testWithName);
            var testWithNameSecoundExample = new Ingredient()
            {
                Name = TestNameForSecoundExample,
                IsDeleted = false,
                IngredientCategoryId = testWithIngredientCategory.Id,
            };

            await this.DbContext.Ingredients.AddAsync(testWithName);
            await this.DbContext.Ingredients.AddAsync(testWithNameSecoundExample);
            await this.DbContext.SaveChangesAsync();

            var list = await this.IngredientsService.GetIngredientsAsync();

            Assert.Equal(2, list.Count);
        }

    }
}
