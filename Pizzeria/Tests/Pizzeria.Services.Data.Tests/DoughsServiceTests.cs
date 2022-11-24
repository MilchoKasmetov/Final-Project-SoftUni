namespace Pizzeria.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using Pizzeria.Data.Common.Repositories;
    using Pizzeria.Data.Models;
    using Pizzeria.Web.ViewModels.Dough;
    using Xunit;

    public class DoughsServiceTests : BaseServiceTests
    {
        private const string TestName = "Test";
        private const string TestNameDough = "Dough";
        private const string TestNameNull = null;

        private IDoughsService DoughsServiceMoq => this.ServiceProvider.GetRequiredService<IDoughsService>();


        [Fact]
        public async Task CreateAllDoughAsyncSuccessfully()
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

            var doughTestWithName = new CreateDoughInputModel()
            {
                Name = TestName,
            };
            var doughTestDough = new CreateDoughInputModel()
            {
                Name = TestNameDough,
            };

            await this.DoughsServiceMoq.CreateDoughAsync(doughTestWithName);
            await this.DoughsServiceMoq.CreateDoughAsync(doughTestWithName);
            await this.DoughsServiceMoq.CreateDoughAsync(doughTestDough);

            var list = await this.DoughsServiceMoq.GetAllDoughsAsync();

            Assert.Equal(2, list.Count);

        }
    }
}
