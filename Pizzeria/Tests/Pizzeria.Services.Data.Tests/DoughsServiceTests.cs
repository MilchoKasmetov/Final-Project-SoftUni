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
        public async Task CreateAndGetAllDoughAsyncSuccessfully()
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

            Assert.Equal(1, this.DoughsServiceMoq.GetDoughsAsync().Result.Count);
        }
    }
}
