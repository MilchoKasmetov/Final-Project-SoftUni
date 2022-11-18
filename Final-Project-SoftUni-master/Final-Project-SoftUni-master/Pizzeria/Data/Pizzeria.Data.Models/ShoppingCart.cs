namespace Pizzeria.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
