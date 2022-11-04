namespace Pizzeria.Web.ViewModels.Ingredients
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Pizzeria.Web.ViewModels.IngredientCategories;

    public class CreateIngredientInputModel : BaseIngredientInputModel
    {
        public ICollection<IngredientCategoryViewModel> IngredientCategories { get; set; }
    }
}
