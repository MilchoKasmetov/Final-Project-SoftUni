namespace Pizzeria.Web.ViewModels.Sauces
{
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseSauceDipInputModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
