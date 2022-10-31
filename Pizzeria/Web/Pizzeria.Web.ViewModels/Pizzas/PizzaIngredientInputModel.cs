namespace Pizzeria.Web.ViewModels.Pizzas
{
    using System.ComponentModel.DataAnnotations;

    public class PizzaIngredientInputModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public string IngredientCategoryName { get; set; }

        public bool Selected { get; set; }

    }
}
