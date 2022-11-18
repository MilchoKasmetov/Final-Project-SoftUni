namespace Pizzeria.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using Pizzeria.Data.Models;

    public class IndexViewModel
    {
        public string Name { get; set; }

        public string ImageURL { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
