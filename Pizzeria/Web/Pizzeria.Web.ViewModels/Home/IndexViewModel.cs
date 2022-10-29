namespace Pizzeria.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Pizzeria.Data.Models;

    public class IndexViewModel
    {
        public string Name { get; set; }

        public string ImageURL { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
