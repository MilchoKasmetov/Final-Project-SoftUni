namespace Pizzeria.Data.Models
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Pizzeria.Data.Common.Models;

    public class Ingredient : BaseDeletableModel<int>
    {
        public Ingredient()
        {
            this.Pizzas = new HashSet<Pizza>();
        }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<Pizza> Pizzas { get; set; }
    }
}
