namespace Pizzeria.Data.Models
{
    using System.Collections;
    using System.Collections.Generic;

    using Pizzeria.Data.Common.Models;

    public class Ingredient : BaseDeletableModel<int>
    {
        public Ingredient()
        {
            this.Pizzas = new HashSet<Pizza>();
        }

        public string Name { get; set; }

        public ICollection<Pizza> Pizzas { get; set; }
    }
}
