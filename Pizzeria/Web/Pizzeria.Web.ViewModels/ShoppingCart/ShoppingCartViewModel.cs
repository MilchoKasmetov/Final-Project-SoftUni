namespace Pizzeria.Web.ViewModels.ShoppingCart
{
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
