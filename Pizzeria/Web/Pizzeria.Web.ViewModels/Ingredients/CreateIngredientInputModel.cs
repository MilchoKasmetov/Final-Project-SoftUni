using Pizzeria.Web.ViewModels.IngredientCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzeria.Web.ViewModels.Ingredients
{
    public class CreateIngredientInputModel : BaseIngredientInputModel
    {
        public ICollection<IngredientCategoryViewModel> IngredientCategories { get; set; }
    }
}
