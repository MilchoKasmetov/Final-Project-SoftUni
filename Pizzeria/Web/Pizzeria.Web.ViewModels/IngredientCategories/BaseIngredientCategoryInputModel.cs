namespace Pizzeria.Web.ViewModels.IngredientCategories
{
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseIngredientCategoryInputModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
