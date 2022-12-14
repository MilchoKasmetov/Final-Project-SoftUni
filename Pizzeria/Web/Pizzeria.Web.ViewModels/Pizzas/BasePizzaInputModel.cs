namespace Pizzeria.Web.ViewModels.Pizzas
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public abstract class BasePizzaInputModel
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

        public PizzaIngredientInputModel[] Ingredients { get; set; }

        public int SizeId { get; set; }

        public ICollection<PizzaSizeInputModel> Sizes { get; set; }

        //[Required]
        public string AddedByUserId { get; set; }

        [Required]
        [Range(0.00, 1000.00)]
        public decimal Price { get; set; }
    }
}
