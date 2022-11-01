namespace Pizzeria.Web.ViewModels.Pizzas
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Pizzeria.Data.Models;

    public class EditPizzaInputModel : BasePizzaInputModel
    {
        public int Id { get; set; }
    }
}
