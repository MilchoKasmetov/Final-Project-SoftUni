namespace Pizzeria.Web.ViewModels.ShoppingCart
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Pizzeria.Data.Models;

    public class ShoppingCartViewModel
    {
        public int ShoppingCartActivityId { get; set; }

        public string Name { get; set; }

        public string ImageURL { get; set; }

        public string Dough { get; set; }

        public string SauceDip { get; set; }

        public string Ingredients { get; set; }

        public string Size { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice => this.Quantity * this.Price;

        public int PizzaId { get; set; }
    }
}
