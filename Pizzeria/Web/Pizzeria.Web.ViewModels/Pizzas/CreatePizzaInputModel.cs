namespace Pizzeria.Web.ViewModels.Pizzas
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Pizzeria.Data.Models;

    public class CreatePizzaInputModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        // bool is meat /isbestSeller/category/price

        [Required]
        public string ImageURL { get; set; }

        public int DoughId { get; set; }

        public ICollection<PizzaDoughInputModel> Doughs { get; set; }

        public int SauceDipId { get; set; }

        public ICollection<PizzaSauceDipInputModel> SauceDips { get; set; }

        public ICollection<PizzaIngredientInputModel> Ingredients { get; set; }

        public int SizeId { get; set; }

        public ICollection<Size> Sizes { get; set; }

        //[Required]
        public string AddedByUserId { get; set; }
    }
}
