namespace Pizzeria.Data.Models
{
    using Pizzeria.Data.Common.Models;

    public class Dough : BaseDeletableModel<int>
    {
        public int Name { get; set; }
    }
}
