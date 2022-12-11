namespace Pizzeria.Web.ViewModels.Ingredients
{
    using System.Collections.Generic;

    using Pizzeria.Web.ViewModels.IngredientCategories;

    public class CreateIngredientInputModel : BaseIngredientInputModel
    {
        public ICollection<IngredientCategoryViewModel> IngredientCategories { get; set; }
    }
}
