namespace Pizzeria.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Pizzeria.Data.Models;
    using Pizzeria.Web.ViewModels.Dough;
    using Xunit;

    public class DoughsServiceTests : BaseServiceTests
    {
        private const int TestId = 1;
        private const int TestIdForSecoundExample = 2;

        private const string TestName = "Test";
        private const string TestNameDough = "Dough";
        private const string TestNameNull = null;

        private IDoughsService DoughsServiceMoq => this.ServiceProvider.GetRequiredService<IDoughsService>();

        [Fact]
        public async Task CreateDoughAsyncSuccessfully()
        {
            var doughTestWithName = new CreateDoughInputModel()
            {
                Name = TestName,
            };
            var doughTestNullName = new CreateDoughInputModel()
            {
                Name = TestNameNull,
            };

            await this.DoughsServiceMoq.CreateDoughAsync(doughTestWithName);
            await this.DoughsServiceMoq.CreateDoughAsync(doughTestWithName);
            await this.DoughsServiceMoq.CreateDoughAsync(doughTestNullName);

            var list = await this.DbContext.Doughs.ToListAsync();

            Assert.Single(list);
            Assert.Equal(TestName, list.FirstOrDefault(x => x.Name == TestName).Name);
        }

        [Fact]
        public async Task GetAllDoughsAsyncSuccessfully()
        {
            var doughTestWithName = new Dough()
            {
                Name = TestName,
                IsDeleted = false,
            };
            await this.DbContext.Doughs.AddAsync(doughTestWithName);
            var doughTestDough = new Dough()
            {
                Name = TestNameDough,
                IsDeleted = false,
            };

            await this.DbContext.Doughs.AddAsync(doughTestWithName);
            await this.DbContext.Doughs.AddAsync(doughTestDough);
            await this.DbContext.SaveChangesAsync();

            var list = await this.DoughsServiceMoq.GetAllDoughsAsync();

            Assert.Equal(2, list.Count);
        }

        [Fact]
        public async Task GetForUpdateAsyncSuccessfully()
        {
            var doughTestWithName = new Dough()
            {
                Name = TestName,
                IsDeleted = false,
            };
            await this.DbContext.Doughs.AddAsync(doughTestWithName);
            await this.DbContext.SaveChangesAsync();
            var output = await this.DoughsServiceMoq.GetForUpdateAsync(TestId);

            Assert.Equal(TestName, output.Name);
        }

        [Fact]
        public async Task ShowAllDeletedAsyncSuccessfully()
        {
            var doughTestWithName = new Dough()
            {
                Name = TestName,
                IsDeleted = false,
            };
            var doughTestDough = new Dough()
            {
                Name = TestNameDough,
                IsDeleted = true,
            };

            await this.DbContext.Doughs.AddAsync(doughTestWithName);
            await this.DbContext.Doughs.AddAsync(doughTestDough);
            await this.DbContext.SaveChangesAsync();
            var output = await this.DoughsServiceMoq.ShowAllDeletedAsync();

            Assert.Equal(1, output.Count);
            Assert.Equal(TestNameDough,output.FirstOrDefault( x => x.Name == TestNameDough).Name);
            Assert.Equal(2, output.FirstOrDefault( x => x.Id == TestIdForSecoundExample).Id);
        }

        [Fact]
        public async Task UpdateAsyncSuccessfully()
        {
            var doughTestWithName = new Dough()
            {
                Name = TestName,
            };

            var doughTestDough = new EditDoughInputModel()
            {
                Name = TestNameDough,
            };

            await this.DbContext.Doughs.AddAsync(doughTestWithName);
            await this.DbContext.SaveChangesAsync();
            await this.DoughsServiceMoq.UpdateAsync(TestId, doughTestDough);

            Assert.Equal(TestNameDough, doughTestWithName.Name);
        }

        [Fact]
        public async Task DeleteSuccessfully()
        {
            var doughTestWithName = new Dough()
            {
                Name = TestName,
            };
            await this.DbContext.Doughs.AddAsync(doughTestWithName);
            await this.DbContext.SaveChangesAsync();
            await this.DoughsServiceMoq.Delete(TestId);

            Assert.Equal(TestName, doughTestWithName.Name);
            Assert.True(doughTestWithName.IsDeleted);
        }

        [Fact]
        public async Task RestoreSuccessfully()
        {
            var doughTestWithName = new Dough()
            {
                Name = TestName,
                IsDeleted = true,
            };
            await this.DbContext.Doughs.AddAsync(doughTestWithName);
            await this.DbContext.SaveChangesAsync();
            await this.DoughsServiceMoq.Restore(1);

            Assert.Equal(TestName, doughTestWithName.Name);
            Assert.False(doughTestWithName.IsDeleted);
        }

        [Fact]
        public async Task GetDoughsAsyncSuccessfully()
        {
            var doughTestWithName = new Dough()
            {
                Name = TestName,
                IsDeleted = false,
            };
            await this.DbContext.Doughs.AddAsync(doughTestWithName);
            var doughTestDough = new Dough()
            {
                Name = TestNameDough,
                IsDeleted = false,
            };

            await this.DbContext.Doughs.AddAsync(doughTestWithName);
            await this.DbContext.Doughs.AddAsync(doughTestDough);
            await this.DbContext.SaveChangesAsync();

            var list = await this.DoughsServiceMoq.GetDoughsAsync();

            Assert.Equal(2, list.Count);
        }
    }
}
