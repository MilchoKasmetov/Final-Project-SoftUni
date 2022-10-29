namespace Pizzeria.Data.Models
{
    using Pizzeria.Data.Common.Models;

    public class Dough : BaseDeletableModel<int>
    {
        public string Name { get; set; }
    }
}
