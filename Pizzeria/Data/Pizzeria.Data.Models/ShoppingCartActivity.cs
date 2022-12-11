namespace Pizzeria.Data.Models
{
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
