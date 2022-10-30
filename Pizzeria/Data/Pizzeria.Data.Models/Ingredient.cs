namespace Pizzeria.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Pizzeria.Data.Common.Models;

    public class Ingredient : BaseDeletableModel<int>
    {
        public Ingredient()
        {
            this.Pizzas = new HashSet<Pizza>();
        }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public int IngredientCategoryId { get; set; }

        public IngredientCategory IngredientCategory { get; set; }

        public ICollection<Pizza> Pizzas { get; set; }
    }
}
