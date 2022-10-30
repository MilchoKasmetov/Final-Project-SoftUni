namespace Pizzeria.Web.ViewModels.Pizzas
{
    using System.ComponentModel.DataAnnotations;

    public class PizzaIngredientInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public int IngredientCategoryId { get; set; }

    }
}
