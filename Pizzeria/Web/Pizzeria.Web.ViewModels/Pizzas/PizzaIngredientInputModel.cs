namespace Pizzeria.Web.ViewModels.Pizzas
{
    using System.ComponentModel.DataAnnotations;

    public class PizzaIngredientInputModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IngredientCategoryName { get; set; }

        public bool Selected { get; set; }

    }
}
