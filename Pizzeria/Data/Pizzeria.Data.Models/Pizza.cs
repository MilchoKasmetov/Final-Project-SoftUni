namespace Pizzeria.Data.Models
{
    using System.Collections.Generic;

    using Pizzeria.Data.Common.Models;

    public class Pizza : BaseDeletableModel<int>
    {
        public Pizza()
        {
            this.Ingredients = new HashSet<Ingredient>();
        }

        public string Name { get; set; }

        public string ImageURL { get; set; }

        // category/pesht/rating/ moje da addna
        public int DoughId { get; set; }

        public Dough Dough { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }

        public int SizeId { get; set; }

        public Size Size { get; set; }

        public int SauceDipId { get; set; }

        public SauceDip SauceDip { get; set; }

        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }
    }
}
