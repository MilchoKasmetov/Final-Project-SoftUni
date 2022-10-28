namespace Pizzeria.Data.Models
{
    using Pizzeria.Data.Common.Models;

    public class SauceDip : BaseDeletableModel<int>
    {
        public string Name { get; set; }
    }
}