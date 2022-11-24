using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzeria.Services.Data.Tests
{
    internal class IngredientCategoriesServiceTests : BaseServiceTests
    {
        private const int TestId = 1;
        private const int TestIdForSecoundExample = 2;

        private const string TestName = "Test";
        private const string TestNameForSecoundExample = "Test two";
        private const string TestNameNull = null;

        private IIngredientCategoriesService IngredientCategoriesService => this.ServiceProvider.GetRequiredService<IIngredientCategoriesService>();


    }
}
