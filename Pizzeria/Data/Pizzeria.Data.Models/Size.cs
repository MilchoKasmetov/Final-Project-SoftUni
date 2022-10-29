namespace Pizzeria.Data.Models
{
    using Pizzeria.Data.Common.Models;

    public class Size : BaseDeletableModel<int>
    {
        public string Name { get; set; }
    }
}
