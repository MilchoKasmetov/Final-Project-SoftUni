namespace Pizzeria.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Pizzeria.Data.Common.Models;

    public class Pizza : BaseDeletableModel<int>
    {
        public Pizza()
        {
            this.Ingredients = new HashSet<Ingredient>();
        }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        // bool is meat /isbestSeller/category/price

        [Required]
        public string ImageURL { get; set; }

        [Required]
        public int DoughId { get; set; }

        [Required]
        public Dough Dough { get; set; }

        [Required]
        public int SauceDipId { get; set; }

        public SauceDip SauceDip { get; set; }

        [Required]
        public ICollection<Ingredient> Ingredients { get; set; }

        [Required]
        public int SizeId { get; set; }

        public Size Size { get; set; }

        [Required]
        [Range(0.00, 1000.00)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        //[Required]
        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }

        public ICollection<ShoppingCartActivity> ShoppingCartActivities { get; set; }
    }
}
