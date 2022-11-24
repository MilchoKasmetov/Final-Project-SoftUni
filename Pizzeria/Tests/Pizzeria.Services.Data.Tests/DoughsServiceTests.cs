using Microsoft.EntityFrameworkCore;
using Moq;
using Pizzeria.Data.Common.Repositories;
using Pizzeria.Data.Models;
using Pizzeria.Web.ViewModels.Dough;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pizzeria.Services.Data.Tests
{
    public class DoughsServiceTests
    {
        private const string TestName = "Test";
        private const string TestNameDough = "Dough";
        private const string TestNameNull = null;

        [Fact]
        public async Task CreateDoughAsyncSuccessfully()
        {
            var list = new List<Dough>();
            var mockRepo = new Mock<IDeletableEntityRepository<Dough>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable<Dough>);
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Dough>())).Callback((Dough dought) => list.Add(dought));

            var service = new DoughsService(mockRepo.Object);

            var doughTestOne = new CreateDoughInputModel()
            {
                Name = TestName,
            };
            var doughTestTwo = new CreateDoughInputModel()
            {
                Name = TestNameNull,
            };

            await service.CreateDoughAsync(doughTestOne);
            await service.CreateDoughAsync(doughTestOne);
            await service.CreateDoughAsync(doughTestTwo);

            mockRepo.Verify(x => x.All(), Times.Exactly(3));
            mockRepo.Verify(x => x.AddAsync(It.IsAny<Dough>()), Times.Exactly(1));

            Assert.Single(list);
            Assert.Equal(TestName, list[0].Name);
        }



        [Fact]
        public async Task GetAllDoughsAsyncSuccessfully()
        {
            var list = new List<Dough>();
            var mockRepo = new Mock<IDeletableEntityRepository<Dough>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable<Dough>);
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Dough>())).Callback((Dough dought) => list.Add(dought));

            var service = new DoughsService(mockRepo.Object);

            var doughTestOne = new CreateDoughInputModel()
            {
                Name = TestName,
            };
            var doughTestTwo = new CreateDoughInputModel()
            {
                Name = TestNameDough,
            };

            await service.CreateDoughAsync(doughTestOne);
            await service.CreateDoughAsync(doughTestOne);
            await service.CreateDoughAsync(doughTestTwo);
            var listToCheck = await service.GetDoughsAsync();

            Assert.Equal(2, listToCheck.Count);
        }
    }
}
