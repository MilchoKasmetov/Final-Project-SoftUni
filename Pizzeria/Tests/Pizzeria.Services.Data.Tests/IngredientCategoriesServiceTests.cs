namespace Pizzeria.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Pizzeria.Data.Models;
    using Pizzeria.Web.ViewModels.IngredientCategories;
    using Xunit;

    public class IngredientCategoriesServiceTests : BaseServiceTests
    {
        private const int TestId = 1;
        private const int TestIdForSecoundExample = 2;

        private const string TestName = "Test";
        private const string TestNameForSecoundExample = "Test two";
        private const string TestNameNull = null;

        private IIngredientCategoriesService IngredientCategoriesService => this.ServiceProvider.GetRequiredService<IIngredientCategoriesService>();

        [Fact]
        public async Task CreateIngredientCategoryAsyncSuccessfully()
        {
            var testWithName = new CreateIngredientCategoriesInputModel()
            {
                Name = TestName,
            };
            var testNullName = new CreateIngredientCategoriesInputModel()
            {
                Name = TestNameNull,
            };

            await this.IngredientCategoriesService.CreateIngredientCategoryAsync(testWithName);
            await this.IngredientCategoriesService.CreateIngredientCategoryAsync(testWithName);
            await this.IngredientCategoriesService.CreateIngredientCategoryAsync(testNullName);

            var list = await this.DbContext.IngredientCategories.ToListAsync();

            Assert.Single(list);
            Assert.Equal(TestName, list.FirstOrDefault(x => x.Name == TestName).Name);
        }

        [Fact]
        public async Task GetIngredientCategoriesAsyncSuccessfully()
        {
            var testWithName = new IngredientCategory()
            {
                Name = TestName,
                IsDeleted = false,
            };
            await this.DbContext.IngredientCategories.AddAsync(testWithName);
            var testWithNameSecoundExample = new IngredientCategory()
            {
                Name = TestNameForSecoundExample,
                IsDeleted = false,
            };

            await this.DbContext.IngredientCategories.AddAsync(testWithName);
            await this.DbContext.IngredientCategories.AddAsync(testWithNameSecoundExample);
            await this.DbContext.SaveChangesAsync();

            var list = await this.IngredientCategoriesService.GetIngredientCategoriesAsync();

            Assert.Equal(2, list.Count);
        }

        [Fact]
        public async Task GetForUpdateAsyncSuccessfully()
        {
            var testWithName = new IngredientCategory()
            {
                Name = TestName,
                IsDeleted = false,
            };
            await this.DbContext.IngredientCategories.AddAsync(testWithName);
            await this.DbContext.SaveChangesAsync();
            var output = await this.IngredientCategoriesService.GetForUpdateAsync(TestId);

            Assert.Equal(TestName, output.Name);
        }

        [Fact]
        public async Task ShowAllDeletedAsyncSuccessfully()
        {
            var testWithName = new IngredientCategory()
            {
                Name = TestName,
                IsDeleted = false,
            };
            await this.DbContext.IngredientCategories.AddAsync(testWithName);
            var testWithNameSecoundExample = new IngredientCategory()
            {
                Name = TestNameForSecoundExample,
                IsDeleted = true,
            };

            await this.DbContext.IngredientCategories.AddAsync(testWithName);
            await this.DbContext.IngredientCategories.AddAsync(testWithNameSecoundExample);
            await this.DbContext.SaveChangesAsync();
            var output = await this.IngredientCategoriesService.ShowAllDeletedAsync();

            Assert.Equal(1, output.Count);
            Assert.Equal(TestNameForSecoundExample, output.FirstOrDefault(x => x.Name == TestNameForSecoundExample).Name);
            Assert.Equal(2, output.FirstOrDefault(x => x.Id == TestIdForSecoundExample).Id);
        }

        [Fact]
        public async Task UpdateAsyncSuccessfully()
        {
            var testWithName = new IngredientCategory()
            {
                Name = TestName,
                IsDeleted = false,
            };

            var testWithNameSecoundExample = new EditIngredientCategoriesInputModel()
            {
                Name = TestNameForSecoundExample,
            };

            await this.DbContext.IngredientCategories.AddAsync(testWithName);
            await this.DbContext.SaveChangesAsync();
            await this.IngredientCategoriesService.UpdateAsync(TestId, testWithNameSecoundExample);

            Assert.Equal(TestNameForSecoundExample, testWithName.Name);
        }

        [Fact]
        public async Task DeleteSuccessfully()
        {
            var testWithName = new IngredientCategory()
            {
                Name = TestName,
                IsDeleted = false,
            };
            await this.DbContext.IngredientCategories.AddAsync(testWithName);
            await this.DbContext.SaveChangesAsync();
            await this.IngredientCategoriesService.Delete(TestId);

            Assert.Equal(TestName, testWithName.Name);
            Assert.True(testWithName.IsDeleted);
        }

        [Fact]
        public async Task RestoreSuccessfully()
        {
            var testWithName = new IngredientCategory()
            {
                Name = TestName,
                IsDeleted = true,
            };
            await this.DbContext.IngredientCategories.AddAsync(testWithName);
            await this.DbContext.SaveChangesAsync();
            await this.IngredientCategoriesService.Restore(TestId);

            Assert.Equal(TestName, testWithName.Name);
            Assert.False(testWithName.IsDeleted);
        }
    }
}
