using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pizzeria.Data.Models;
using Pizzeria.Web.ViewModels.IngredientCategories;
using Pizzeria.Web.ViewModels.SauceDips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pizzeria.Services.Data.Tests
{
    public class SauceDipsServiceTests : BaseServiceTests
    {
        private const int TestId = 1;
        private const int TestIdForSecoundExample = 2;

        private const string TestName = "Test";
        private const string TestNameForSecoundExample = "Test two";
        private const string TestNameNull = null;

        private ISauceDipsService SauceDipsService => this.ServiceProvider.GetRequiredService<ISauceDipsService>();

        [Fact]
        public async Task CreateSauceDipsCategoryAsyncSuccessfully()
        {
            var testWithName = new CreateSauceDipInputModel()
            {
                Name = TestName,
            };
            var testNullName = new CreateSauceDipInputModel()
            {
                Name = TestNameNull,
            };

            await this.SauceDipsService.CreateSauceDipAsync(testWithName);
            await this.SauceDipsService.CreateSauceDipAsync(testWithName);
            await this.SauceDipsService.CreateSauceDipAsync(testNullName);

            var list = await this.DbContext.SauceDips.ToListAsync();

            Assert.Single(list);
            Assert.Equal(TestName, list.FirstOrDefault(x => x.Name == TestName).Name);
        }

        [Fact]
        public async Task GetSauceDipAsyncSuccessfully()
        {
            var testWithName = new SauceDip()
            {
                Name = TestName,
                IsDeleted = false,
            };
            await this.DbContext.SauceDips.AddAsync(testWithName);
            var testWithNameSecoundExample = new SauceDip()
            {
                Name = TestNameForSecoundExample,
                IsDeleted = false,
            };

            await this.DbContext.SauceDips.AddAsync(testWithName);
            await this.DbContext.SauceDips.AddAsync(testWithNameSecoundExample);
            await this.DbContext.SaveChangesAsync();

            var list = await this.SauceDipsService.GetSauceDipsAsync();

            Assert.Equal(2, list.Count);

        }

        [Fact]
        public async Task GetForUpdateAsyncSuccessfully()
        {

            var testWithName = new SauceDip()
            {
                Name = TestName,
                IsDeleted = false,
            };
            await this.DbContext.SauceDips.AddAsync(testWithName);
            await this.DbContext.SaveChangesAsync();
            var output = await this.SauceDipsService.GetForUpdateAsync(TestId);

            Assert.Equal(TestName, output.Name);
        }



        [Fact]
        public async Task ShowAllDeletedAsyncSuccessfully()
        {
            var testWithName = new SauceDip()
            {
                Name = TestName,
                IsDeleted = false,
            };
            await this.DbContext.SauceDips.AddAsync(testWithName);
            var testWithNameSecoundExample = new SauceDip()
            {
                Name = TestNameForSecoundExample,
                IsDeleted = true,
            };

            await this.DbContext.SauceDips.AddAsync(testWithName);
            await this.DbContext.SauceDips.AddAsync(testWithNameSecoundExample);
            await this.DbContext.SaveChangesAsync();
            var output = await this.SauceDipsService.ShowAllDeletedAsync();

            Assert.Equal(1, output.Count);
            Assert.Equal(TestNameForSecoundExample, output.FirstOrDefault(x => x.Name == TestNameForSecoundExample).Name);
            Assert.Equal(2, output.FirstOrDefault(x => x.Id == TestIdForSecoundExample).Id);
        }


        [Fact]
        public async Task UpdateAsyncSuccessfully()
        {
            var testWithName = new SauceDip()
            {
                Name = TestName,
                IsDeleted = false,
            };

            var testWithNameSecoundExample = new EditSauceDipInputModel()
            {
                Name = TestNameForSecoundExample,
            };

            await this.DbContext.SauceDips.AddAsync(testWithName);
            await this.DbContext.SaveChangesAsync();
            await this.SauceDipsService.UpdateAsync(TestId, testWithNameSecoundExample);

            Assert.Equal(TestNameForSecoundExample, testWithName.Name);
        }

        [Fact]
        public async Task DeleteSuccessfully()
        {
            var testWithName = new SauceDip()
            {
                Name = TestName,
                IsDeleted = false,
            };
            await this.DbContext.SauceDips.AddAsync(testWithName);
            await this.DbContext.SaveChangesAsync();
            await this.SauceDipsService.Delete(TestId);

            Assert.Equal(TestName, testWithName.Name);
            Assert.True(testWithName.IsDeleted);
        }

        [Fact]
        public async Task RestoreSuccessfully()
        {
            var testWithName = new SauceDip()
            {
                Name = TestName,
                IsDeleted = true,
            };
            await this.DbContext.SauceDips.AddAsync(testWithName);
            await this.DbContext.SaveChangesAsync();
            await this.SauceDipsService.Restore(TestId);

            Assert.Equal(TestName, testWithName.Name);
            Assert.False(testWithName.IsDeleted);
        }

        [Fact]
        public async Task GetAllSauceDipAsyncSuccessfully()
        {
            var testWithName = new SauceDip()
            {
                Name = TestName,
                IsDeleted = false,
            };
            await this.DbContext.SauceDips.AddAsync(testWithName);
            var testWithNameSecoundExample = new SauceDip()
            {
                Name = TestNameForSecoundExample,
                IsDeleted = false,
            };

            await this.DbContext.SauceDips.AddAsync(testWithName);
            await this.DbContext.SauceDips.AddAsync(testWithNameSecoundExample);
            await this.DbContext.SaveChangesAsync();

            var list = await this.SauceDipsService.GetAllSauceDipsAsync();

            Assert.Equal(2, list.Count);

        }
    }
}
