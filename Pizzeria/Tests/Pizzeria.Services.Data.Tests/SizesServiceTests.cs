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
    using Pizzeria.Web.ViewModels.Sizes;
    using Xunit;

    public class SizesServiceTests : BaseServiceTests
    {
        private const int TestId = 1;
        private const int TestIdForSecoundExample = 2;

        private const string TestName = "Test";
        private const string TestNameForSecoundExample = "Test two";
        private const string TestNameNull = null;

        private ISizesService SizesService => this.ServiceProvider.GetRequiredService<ISizesService>();

        [Fact]
        public async Task CreateSizeAsyncSuccessfully()
        {
            var testWithName = new CreateSizeInputModel()
            {
                Name = TestName,
            };
            var testNullName = new CreateSizeInputModel()
            {
                Name = TestNameNull,
            };

            await this.SizesService.CreateSizeAsync(testWithName);
            await this.SizesService.CreateSizeAsync(testWithName);
            await this.SizesService.CreateSizeAsync(testNullName);

            var list = await this.DbContext.Sizes.ToListAsync();

            Assert.Single(list);
            Assert.Equal(TestName, list.FirstOrDefault(x => x.Name == TestName).Name);
        }

        [Fact]
        public async Task GetSizeAsync()
        {
            var testWithName = new Size()
            {
                Name = TestName,
                IsDeleted = false,
            };
            await this.DbContext.Sizes.AddAsync(testWithName);
            var testWithNameSecoundExample = new Size()
            {
                Name = TestNameForSecoundExample,
                IsDeleted = false,
            };

            await this.DbContext.Sizes.AddAsync(testWithName);
            await this.DbContext.Sizes.AddAsync(testWithNameSecoundExample);
            await this.DbContext.SaveChangesAsync();

            var list = await this.SizesService.GetSizeAsync();

            Assert.Equal(2, list.Count);
        }

        [Fact]
        public async Task GetForUpdateAsyncSuccessfully()
        {

            var testWithName = new Size()
            {
                Name = TestName,
                IsDeleted = false,
            };
            await this.DbContext.Sizes.AddAsync(testWithName);
            await this.DbContext.SaveChangesAsync();
            var output = await this.SizesService.GetForUpdateAsync(TestId);

            Assert.Equal(TestName, output.Name);
        }

        [Fact]
        public async Task ShowAllDeletedAsyncSuccessfully()
        {
            var testWithName = new Size()
            {
                Name = TestName,
                IsDeleted = false,
            };
            await this.DbContext.Sizes.AddAsync(testWithName);
            var testWithNameSecoundExample = new Size()
            {
                Name = TestNameForSecoundExample,
                IsDeleted = true,
            };

            await this.DbContext.Sizes.AddAsync(testWithName);
            await this.DbContext.Sizes.AddAsync(testWithNameSecoundExample);
            await this.DbContext.SaveChangesAsync();
            var output = await this.SizesService.ShowAllDeletedAsync();

            Assert.Equal(1, output.Count);
            Assert.Equal(TestNameForSecoundExample, output.FirstOrDefault(x => x.Name == TestNameForSecoundExample).Name);
            Assert.Equal(2, output.FirstOrDefault(x => x.Id == TestIdForSecoundExample).Id);
        }

        [Fact]
        public async Task UpdateAsyncSuccessfully()
        {
            var testWithName = new Size()
            {
                Name = TestName,
                IsDeleted = false,
            };

            var testWithNameSecoundExample = new EditSizeInputModel()
            {
                Name = TestNameForSecoundExample,
            };

            await this.DbContext.Sizes.AddAsync(testWithName);
            await this.DbContext.SaveChangesAsync();
            await this.SizesService.UpdateAsync(TestId, testWithNameSecoundExample);

            Assert.Equal(TestNameForSecoundExample, testWithName.Name);
        }

        [Fact]
        public async Task DeleteSuccessfully()
        {
            var testWithName = new Size()
            {
                Name = TestName,
                IsDeleted = false,
            };
            await this.DbContext.Sizes.AddAsync(testWithName);
            await this.DbContext.SaveChangesAsync();
            await this.SizesService.Delete(TestId);

            Assert.Equal(TestName, testWithName.Name);
            Assert.True(testWithName.IsDeleted);
        }

        [Fact]
        public async Task RestoreSuccessfully()
        {
            var testWithName = new Size()
            {
                Name = TestName,
                IsDeleted = true,
            };
            await this.DbContext.Sizes.AddAsync(testWithName);
            await this.DbContext.SaveChangesAsync();
            await this.SizesService.Restore(TestId);

            Assert.Equal(TestName, testWithName.Name);
            Assert.False(testWithName.IsDeleted);
        }

        [Fact]
        public async Task GetAllSizesAsync()
        {
            var testWithName = new Size()
            {
                Name = TestName,
                IsDeleted = false,
            };
            await this.DbContext.Sizes.AddAsync(testWithName);
            var testWithNameSecoundExample = new Size()
            {
                Name = TestNameForSecoundExample,
                IsDeleted = false,
            };

            await this.DbContext.Sizes.AddAsync(testWithName);
            await this.DbContext.Sizes.AddAsync(testWithNameSecoundExample);
            await this.DbContext.SaveChangesAsync();

            var list = await this.SizesService.GetAllSizesAsync();

            Assert.Equal(2, list.Count);
        }
    }
}
