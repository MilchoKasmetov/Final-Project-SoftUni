namespace Pizzeria.Data.Models
{
    using System;
    using System.Collections.Generic;

    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Pizzeria.Data.Common.Models;

    public class Pizza : BaseDeletableModel<int>
    {
        public Pizza()
        {
            this.Ingredients = new HashSet<Ingredient>();
        }

        public string Name { get; set; }

        public string ImageURL { get; set; }

        public int DoughId { get; set; }

        public Dough Dough { get; set; }

        public int SauceDipId { get; set; }

        public SauceDip SauceDip { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }

        public int SizeId { get; set; }

        public Size Size { get; set; }

        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }
    }
}
