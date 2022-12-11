namespace Pizzeria.Web.ViewModels.Sizes
{
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseSizeInputModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
