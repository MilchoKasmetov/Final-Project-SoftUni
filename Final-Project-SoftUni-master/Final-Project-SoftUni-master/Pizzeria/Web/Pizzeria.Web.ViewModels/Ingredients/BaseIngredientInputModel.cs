namespace Pizzeria.Web.ViewModels.Ingredients
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Pizzeria.Data.Models;
    using Pizzeria.Web.ViewModels.IngredientCategories;

    public abstract class BaseIngredientInputModel
    {

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public int IngredientCategoryId { get; set; }

        public IngredientCategoryViewModel IngredientCategory { get; set; }
    }
}
