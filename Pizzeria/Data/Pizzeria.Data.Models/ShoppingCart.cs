namespace Pizzeria.Data.Models
{
    using System.Collections.Generic;

    using Pizzeria.Data.Common.Models;

    public class ShoppingCart : BaseDeletableModel<int>
    {
        public ShoppingCart()
        {
            this.ShoppingCartActivities = new HashSet<ShoppingCartActivity>();
        }

        public ApplicationUser User { get; set; }

        public ICollection<ShoppingCartActivity> ShoppingCartActivities { get; set; }
    }
}
