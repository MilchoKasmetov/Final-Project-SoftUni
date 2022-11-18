namespace Pizzeria.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Pizzeria.Data.Common.Models;

    public class ShoppingCartActivity : BaseDeletableModel<int>
    {
        public int ShoppingCartId { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }

        public int PizzaId { get; set; }

        public virtual Pizza Pizza { get; set; }

        public int Quantity { get; set; }
    }
}
